using System.Linq;
using Domain.EyeConditions;
using Persistence;
using Shared.EyeConditions;
using Microsoft.EntityFrameworkCore;
using Domain.Files;
using Services.Files;

namespace Services.EyeConditions;
public class EyeConditionService : IEyeConditionService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IStorageService storageService;


    public EyeConditionService(ApplicationDbContext context, IStorageService storageService)
    {
        dbContext = context;
        this.storageService = storageService;
    }

    public async Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate model)
    {
        if (await dbContext.EyeConditions.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(EyeCondition), nameof(EyeCondition.Name), model.Name);

        Image image = new Image(storageService.BasePath, model.ImageContentType!);

        //EyeCondition eyeCondition = new(model.Name, model.Description, model.Body, image.FileUri.ToString(), model.BrochureUrl!);
        EyeCondition eyeCondition;
        if (model.BrochureUrl != null) { 
            string originalLink = model.BrochureUrl;
            string downloadLink = ConvertToDownloadLink(originalLink);
            eyeCondition = new(model.Name!, model.Description!, model.Body, image.FileUri.ToString(), downloadLink);
        } else
        {
            eyeCondition = new(model.Name!, model.Description!, model.Body, image.FileUri.ToString(), "");
        }
        

        var symptoms = await dbContext.Symptoms.Where(x => model.Symptoms.Select(s => s.Id).ToList().Contains(x.Id)).ToListAsync();

        foreach (var symptom in symptoms)
        {
            eyeCondition.Symptom(symptom);
        }

        dbContext.EyeConditions.Add(eyeCondition);

        var selectedSymptoms = await dbContext.Symptoms
                                    //.Where(symptom => model.SymptomsChosen.Contains(symptom.Id))
                                    .ToListAsync();


            //eyeCondition.EyeConditionSymptoms.Add(es);
            //dbContext.Symptoms.Add(es);

        await dbContext.SaveChangesAsync();

        // Bug
        Uri uploadSas = storageService.GenerateImageUploadSas(image);

        EyeConditionResult.Create result = new()
        {
            EyeConditionId = eyeCondition.Id,
            Name = eyeCondition.Name,
            Description = eyeCondition.Description,
            Body = eyeCondition.Body,
            UploadUri = uploadSas.ToString(),
            BrochureUrl = eyeCondition.BrochureUrl
        };

        return result;
    }

    public async Task<EyeConditionResult.Index> GetIndexAsync(EyeConditionRequest.Index request)
    {
        var searchTerm = request.Searchterm != null ? request.Searchterm.ToLowerInvariant() : null;

        var query = dbContext.EyeConditions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
        }
        if (request.SymptomIds is not null)
        {
            query = query.Where(x => x.Symptoms.Any(s => request.SymptomIds.Contains(s.Id)));
        }

        int totalAmount = await query.CountAsync();


        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderBy(x => x.Id)
            .Select(x => new EyeConditionDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Symptoms = x.Symptoms.Select(s => new SymptomDto.Index
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList(),
            })
            .ToListAsync();

        var result = new EyeConditionResult.Index
        {
            EyeConditions = items,
            TotalAmount = totalAmount
        };

        return result;
    }


    public async Task<EyeConditionDto.Detail> GetDetailAsync(long Id)
    {
        EyeConditionDto.Detail? product = await dbContext.EyeConditions.Select(x => new EyeConditionDto.Detail
        {
            Id = x.Id,
            Name = x.Name,
            Body = x.Body,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            BrochureUrl = x.BrochureUrl,

        }).SingleOrDefaultAsync(x => x.Id == Id);

        if (product is null)
            throw new EntityNotFoundException(nameof(EyeCondition), Id);

        return product;
    }

    public async Task<EyeConditionDto.Mutate> GetMutateAsync(long Id)
    {
        EyeConditionDto.Mutate? product = await dbContext.EyeConditions
            .Select(x => new EyeConditionDto.Mutate
            {
                Id = x.Id,
                Name = x.Name,
                Body = x.Body,
                Description = x.Description,
                ImageContentType = x.ImageUrl,
                Symptoms = x.Symptoms.Select(s => new SymptomDto.Index
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList(),
            })
            .SingleOrDefaultAsync(x => x.Id == Id);

        if (product is null)
            throw new EntityNotFoundException(nameof(EyeCondition), Id);

        return product;
    }


    public async Task EditAsync(long id, EyeConditionDto.Mutate model)
    {
        EyeCondition? eyeCondition = await dbContext.EyeConditions.SingleOrDefaultAsync(x => x.Id == id);

        if (eyeCondition is null)
            throw new EntityNotFoundException(nameof(EyeCondition), id);

        eyeCondition.Name = model.Name;
        eyeCondition.Description = model.Description;
        //eyeCondition.ImageUrl = model.ImageUrl;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        EyeCondition? eyecondition = await dbContext.EyeConditions.SingleOrDefaultAsync(x => x.Id == id);

        if (eyecondition is null)
            throw new EntityNotFoundException(nameof(EyeCondition),id);

        dbContext.EyeConditions.Remove(eyecondition);

        await dbContext.SaveChangesAsync();
    }

    // public void UpdateEyeCondition(int id, EyeCondition updatedEyeCondition)
    // {

    //     EyeCondition? existingEyeCondition = _context.EyeConditions.FirstOrDefault(e => e.Id == id);

    //     if (existingEyeCondition != null)
    //     {

    //         existingEyeCondition.Name = updatedEyeCondition.Name;
    //         existingEyeCondition.Description = updatedEyeCondition.Description;


    //         existingEyeCondition.Updated();
    //         _context.SaveChanges();
    //     }
    // }

    static string ConvertToDownloadLink(string originalLink)
    {
        const string fileMarker = "https://drive.google.com/file/d/";
        const string viewMarker = "/view?usp=sharing";
        const string downloadEndpoint = "https://drive.google.com/uc?export=download&id=";

        string temp = originalLink.Replace(fileMarker, "");
        temp = temp.Replace(viewMarker, "");
        temp = downloadEndpoint + temp;
        return temp;
    }
}
