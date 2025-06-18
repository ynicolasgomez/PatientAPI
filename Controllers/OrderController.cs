using Microsoft.AspNetCore.Mvc;
using PatientAPI.Infrastructure;
using PatientAPI.Models;
using System.Threading.Tasks;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _repository.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _repository.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest();
            await _repository.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetToday()
        {
            var todayOrders = await _repository.GetTodayOrdersAsync();
            return Ok(todayOrders);
        }

        [HttpGet("count-by-patient")]
        public async Task<IActionResult> GetCountPerPatient()
        {
            var result = await _repository.GetOrderCountPerPatientAsync();
            return Ok(result);
        }
    }
}
