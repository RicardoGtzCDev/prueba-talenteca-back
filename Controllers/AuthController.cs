
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _dbContext = context;
        }

        [HttpPost]
        public IActionResult Post(GenerarJwtDTO input)
        {
            try
            {
                Cliente? cliente = _dbContext
                    .Clientes
                    .FirstOrDefault(c =>
                        c.Email == input.Email
                        && c.Contraseña == input.Contraseña
                    );
                if (cliente == null)
                {
                    return Unauthorized("Usuario/Contraseña");
                }
                AuthClaims claims = new AuthClaims
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    NombreCompleto = $"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}"
                };
                string? token = GenerateJwt(claims);
                if (token == null)
                {
                    return BadRequest("No se ha podido generar el token");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return BadRequest("No se ha podido generar el token");
                }
                AuthClaims? claims = GetClaims(identity);
                if (claims == null)
                {
                    return BadRequest("No se ha podido generar el token");
                }
                string? token = GenerateJwt(claims);
                if (token == null)
                {
                    return BadRequest("No se ha podido generar el token");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string? GenerateJwt(AuthClaims input)
        {
            try
            {
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_SECRET_KEY"]));
                SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                Claim[] claims = new[]
                    {
                        new Claim("id", input.Id.ToString()),
                        new Claim("correo", input.Email),
                        new Claim("nombre", input.NombreCompleto)
                    };
                JwtSecurityToken securityToken = new JwtSecurityToken(null, null, claims, null, DateTime.Now.AddHours(3), credentials); ;
                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return token;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private AuthClaims? GetClaims(ClaimsIdentity identity)
        {
            string? id = identity.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();
            string? email = identity.Claims.Where(c => c.Type == "correo").Select(c => c.Value).SingleOrDefault();
            string? nombre = identity.Claims.Where(c => c.Type == "nombre").Select(c => c.Value).SingleOrDefault();
            if (id != null && email != null && nombre != null)
            {
                return new AuthClaims()
                {
                    Id = Int32.Parse(id),
                    Email = email,
                    NombreCompleto = nombre
                };
            }
            return null;
        }

    }

}
