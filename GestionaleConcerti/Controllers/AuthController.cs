using System.Threading.Tasks;
using System;
using GestionaleConcerti.Models;
using GestionaleConcerti.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionaleConcerti.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var token = await _authService.RegisterAsync(
                request.Name,
                request.Surname,
                request.Email,
                request.Password,
                request.BirthDate,
                role: "User"
            );

            if (token == null)
                return BadRequest("Registrazione fallita.");

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (token == null)
                return Unauthorized("Email o password errati.");

            return Ok(new { Token = token });
        }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
