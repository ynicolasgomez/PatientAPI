using Microsoft.AspNetCore.Mvc;
using PatientAPI.Infrastructure.DocumentType;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly DocumentTypeRepository _repository;

        public DocumentTypeController(DocumentTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
    }
}
