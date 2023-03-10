using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Common.DTO;
using Contracts.Service;
using DataAccess;

namespace UsersCRUD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutenticacionController : ControllerBase
{
    private readonly string secretKey;
    private readonly UserContext _context;

    public AutenticacionController(IConfiguration config, UserContext userContext)
    {
        secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
        _context = userContext;
    }

    [HttpPost]
    [Route("Validate")]
    public IActionResult Validate([FromBody] UserLogin userLogin)
    {

        if (_context.Users.Any(u => u.Email == userLogin.Email && u.Password == userLogin.Password))
        {

            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();

            // Agregar reclamo de nombre de usuario
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userLogin.Email));

            // Buscar el rol del usuario en la base de datos y agregar el reclamo de rol correspondiente
            var user = _context.Users.Single(u => u.Email == userLogin.Email);
            if (user.Role == "Admin")
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if (user.Role == "UserOnly")
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, "UserOnly"));
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);


            return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

        }
        else
        {

            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
        }
    }
}
