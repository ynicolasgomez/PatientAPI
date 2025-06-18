using Microsoft.AspNetCore.Mvc;
using PatientAPI.Models;

namespace PatientAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "demo" && request.Password == "1234")
            {
                // Simulación de token
                var fakeToken = "FAKE-TOKEN-123456";
                return Ok(new { token = fakeToken });
            }

            return Unauthorized(new { message = "Credenciales inválidas" });
        }
    }
}
