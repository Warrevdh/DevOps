using Microsoft.EntityFrameworkCore;
using Domain.EyeConditions;
using Persistence;
using Shared.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.EyeConditions;

public class SymptomService : ISymptomService
{
    private readonly ApplicationDbContext dbContext;

    public SymptomService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<SymptomResult.Create> CreateAsync(SymptomDto.Mutate model)
    {
        if (await dbContext.Symptoms.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(EyeCondition), nameof(EyeCondition.Name), model.Name);

        Symptom symptom = new(model.Name);
        
        dbContext.Symptoms.Add(symptom);
        await dbContext.SaveChangesAsync();

        return new SymptomResult.Create
        {
            SymptomId = symptom.Id,
            Name = symptom.Name,
        };


    }
    public async Task DeleteAsync(long id)
    {
        Symptom? symptom = await dbContext.Symptoms.SingleOrDefaultAsync(x => x.Id == id);

        if (symptom is null)
            throw new EntityNotFoundException(nameof(EyeCondition), id);

        dbContext.Symptoms.Remove(symptom);

        await dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(long symptomId, SymptomDto.Mutate model)
    {
        Symptom? symptom = await dbContext.Symptoms.SingleOrDefaultAsync(x => x.Id == symptomId);

        if (symptom is null)
        {
            throw new EntityNotFoundException(nameof(symptom), symptomId);
        }

        symptom.Name = model.Name!;

        await dbContext.SaveChangesAsync();
    }

    public async Task<SymptomResult.Index> GetIndexAsync(SymptomRequest.Index request)
    {
        var query = dbContext.Symptoms.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }

        int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new SymptomDto.Index
           {
               Id = x.Id,
               Name = x.Name,
           }).ToListAsync();
        
        var result = new SymptomResult.Index
        {
            Symptoms = items,
            TotalAmount = totalAmount
        };
        return result;
    }
}
