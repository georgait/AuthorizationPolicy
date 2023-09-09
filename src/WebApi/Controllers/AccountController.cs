using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return BadRequest();
        }

        List<Claim> claims;

        if (userName == "alice")
        {
            claims = new List<Claim>
            {
                new Claim("sub", "1"),
                new Claim("name", "Alice"),
                new Claim("role", "user"),
            };
        }
        else if (userName == "bob")
        {
            claims = new List<Claim>
            {
                new Claim("sub", "11"),
                new Claim("name", "Bob"),
                new Claim("role", "tipster"),
            };
        }
        else
        {
            return BadRequest();
        }

        var identity = new ClaimsIdentity(claims, "password", "name", "role");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(principal);

        return Ok(claims);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}
