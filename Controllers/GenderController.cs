using Microsoft.AspNetCore.Mvc;
using PatientAPI.Infrastructure;
using PatientAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class GenderController : ControllerBase
{
    private readonly GenderRepository _genderRepository;

    public GenderController(GenderRepository genderRepository)
    {
        _genderRepository = genderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gender>>> GetAll()
    {
        var genders = await _genderRepository.GetAllAsync();
        return Ok(genders);
    }
}
