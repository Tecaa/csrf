using csrf_example_net.Requests;
using csrf_example_net.Responses;
using Microsoft.AspNetCore.Mvc;

namespace csrf_example_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        [IgnoreAntiforgeryToken]  //CSRF vulnerability
        [HttpPost("login")]
        public IActionResult Login(LoginRq request)
        {
            // Perform dummy login logic here

            if (IsValidCredentials(request.Username, request.Password))
            {
                // Successful login
                // Set session cookie
                string sessionValue = "your_session_value";
                HttpContext.Response.Cookies.Append("session", sessionValue);

                // Save session value in server-side
                _httpContextAccessor.HttpContext?.Session?.SetString("session", sessionValue);

                return Ok();
            }
            else
            {
                // Failed login
                return Unauthorized();
            }
        }

        private bool IsValidCredentials(string username, string password)
        {
            // Replace this logic with your actual login validation logic
            // For demonstration purposes, we are using hard-coded values

            string validUsername = "admin";
            string validPassword = "password";

            return username == validUsername && password == validPassword;
        }
    }
}
