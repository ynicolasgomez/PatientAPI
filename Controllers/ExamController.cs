using Microsoft.AspNetCore.Mvc;
using PatientAPI.Infrastructure;
using PatientAPI.Models;
using System.Threading.Tasks;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly ExamRepository _repository;

        public ExamController(ExamRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exams = await _repository.GetAllAsync(); 
            return Ok(exams);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Exam exam)
        {
            await _repository.AddAsync(exam); 
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }
    }
}
