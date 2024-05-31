using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Users.Patients;

namespace Services.Users.Patients;
public class PatientService : IPatientService
{
    private readonly ApplicationDbContext dbContext;

    public PatientService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

	public async Task<PatientResult.Index> GetIndexAsync(PatientRequest.Index request)
    {
        var query = dbContext.Patients.AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchtermName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchtermName));
		}

		if (!string.IsNullOrWhiteSpace(request.SearchtermEmail))
		{
			query = query.Where(x => x.Email.Contains(request.SearchtermEmail));
		}


		int totalAmount = await query.CountAsync();

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new PatientDto.Index
           {
               Id = x.Id,
               FirstName = x.FirstName,
               LastName = x.LastName,
               Email = x.Email,
           }).ToListAsync();

        var result = new PatientResult.Index
        {
            Patients = items,
            TotalAmount = totalAmount
        };
        return result;
    }

    public async Task<PatientDto.Detail> GetDetailAsync(long patientId)
	{
		PatientDto.Detail? patient = await dbContext.Patients.Select(x => new PatientDto.Detail
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            BirthDate = x.BirthDate,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
        }).SingleOrDefaultAsync(x => x.Id == patientId);

        if (patient == null)
        {
            throw new EntityNotFoundException(nameof(patient), patientId);
        }

        return patient;
	}
}
