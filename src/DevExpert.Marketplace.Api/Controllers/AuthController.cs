using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevExpert.Marketplace.Core.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DevExpert.Marketplace.Api.Controllers;

public class AuthController(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]LoginInputViewModel inputViewModel)
    {
        var user = await userManager.FindByNameAsync(inputViewModel.Email);
        if (user == null)
            return Unauthorized(new { Error = "Usuário ou senha incorretos." });

        var result = await signInManager.CheckPasswordSignInAsync(user, inputViewModel.Password, false);
        if (!result.Succeeded)
            return Unauthorized(new { Error = "Usuário ou senha incorretos." });

        var token = GenerateAccessToken(user);

        return Ok(new { Token = token });
    }

    private string GenerateAccessToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Jwt.Instance!.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.UserName!),
                new("UserId", user.Id)
            }),
            Issuer = Jwt.Instance!.Issuer,
            Audience = Jwt.Instance!.Audience,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(Jwt.Instance!.ExpireMinutes),
            IssuedAt = DateTime.UtcNow,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            TokenType = Jwt.Instance!.TokenTypeAccessToken,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}