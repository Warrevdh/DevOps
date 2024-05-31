using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users.Patients;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Users.Patients;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PatientController : ControllerBase
{

    private readonly IPatientService patientService;

    public PatientController(IPatientService patientService)
    {
        this.patientService = patientService;
    }

    [SwaggerOperation("Returns a list of patients")]
    [HttpGet, AllowAnonymous]
    public async Task<PatientResult.Index> GetIndex([FromQuery] PatientRequest.Index request)
    {
        return await patientService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific patient.")]
    [HttpGet("{patientId}"), AllowAnonymous]
    public async Task<PatientDto.Detail> GetDetail(long patientId)
    {
        return await patientService.GetDetailAsync(patientId);
    }

    // private static List<Patient> patients = new List<Patient>();

    // [HttpGet]
    // public IActionResult GetAllPatients()
    // {
    //     return Ok(patients);
    // }

    // [HttpGet("{id}")]
    // public IActionResult GetPatient(int id)
    // {
    //     var patient = patients.FirstOrDefault(p => p.PatientId == id);
    //     if (patient == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(patient);
    // }

    // [HttpPost]
    // public IActionResult CreatePatient([FromBody] Patient patient)
    // {
    //     if (patient == null)
    //     {
    //         return BadRequest("Invalid data");
    //     }

    //     patients.Add(patient);

    //     return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
    // }
}
