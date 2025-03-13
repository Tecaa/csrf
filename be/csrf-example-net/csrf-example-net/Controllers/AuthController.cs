using csrf_example_net.Requests;
using csrf_example_net.Responses;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace csrf_example_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthController> _logger;
        private readonly IAntiforgery _antiforgery;

        public AuthController(ILogger<AuthController> logger, IHttpContextAccessor httpContextAccessor, IAntiforgery antiforgery)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _antiforgery = antiforgery;
        }


        [IgnoreAntiforgeryToken]  //CSRF vulnerability
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRq request)
        {
            // Perform dummy login logic here

            if (IsValidCredentials(request.Username, request.Password))
            {
                // Here you would validate the username and password against your user store
                // For demonstration, let's assume the user is valid

                var claims = new List<Claim>
                     {
                         new Claim(ClaimTypes.Name, request.Username)
                     };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                //await HttpContext.SignOutAsync();
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);
                this.HttpContext.User = new ClaimsPrincipal(claimsIdentity);

                var tokenSet = _antiforgery.GetAndStoreTokens(this.HttpContext);
                
                this.HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!,
                    new CookieOptions { HttpOnly = false /*, SameSite = SameSiteMode.None, Secure = true*/ });




                return Ok();
            }
            else
            {
                // Failed login
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return StatusCode(200, "OK");
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("denied")]
        public IActionResult AccessDenied()
        {
            return StatusCode(403, "Access Denied");
        }

        private bool IsValidCredentials(string username, string password)
        {
            // Replace this logic with your actual login validation logic
            // For demonstration purposes, we are using hard-coded values

            string validUsername = "admin";
            string validPassword = "q1s2d3v4b5";

            return username == validUsername && password == validPassword;
        }
    }
}
