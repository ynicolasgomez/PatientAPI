using Microsoft.AspNetCore.Mvc;
using PatientAPI.Infrastructure;
using PatientAPI.Models;

namespace PatientAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientRepository _repository;

    public PatientController(PatientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var patient = await _repository.GetByIdAsync(id);
        return patient == null ? NotFound() : Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        await _repository.AddAsync(patient);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Patient patient)
    {
        patient.Id = id;
        await _repository.UpdateAsync(patient);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string filter)
    {
        var results = await _repository.SearchAsync(filter);
        return Ok(results);
    }

}
