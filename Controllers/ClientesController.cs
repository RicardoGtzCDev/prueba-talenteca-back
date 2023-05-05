using Microsoft.AspNetCore.Mvc;
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;

namespace Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ClientesController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CrearClienteDTO input)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    Email = input.Email,
                    Contraseña = input.Contraseña,
                    Nombre = input.Nombre,
                    ApellidoPaterno = input.ApellidoPaterno,
                    ApellidoMaterno = input.ApellidoMaterno,
                    Direccion = input.Direccion,
                };
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();
                ClienteResponse response = new ClienteResponse
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Nombre = cliente.Nombre,
                    ApellidoPaterno = cliente.ApellidoPaterno,
                    ApellidoMaterno = cliente.ApellidoMaterno,
                    Direccion = cliente.Direccion,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                List<ClienteResponse> clientes = _dbContext
                    .Clientes
                    .Select(c => new ClienteResponse
                    {
                        Id = c.Id,
                        Email = c.Email,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoPaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                        Direccion = c.Direccion,    
                    })
                    .ToList();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                Cliente? cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null) { return NotFound(); }
                ClienteResponse response = new ClienteResponse
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Nombre = cliente.Nombre,
                    ApellidoPaterno = cliente.ApellidoPaterno,
                    ApellidoMaterno = cliente.ApellidoMaterno,
                    Direccion = cliente.Direccion,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult Patch(int id, ActualizarClienteDTO input)
        {
            try
            {
                Cliente? cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null) { return NotFound(); }
                cliente.Email = String.IsNullOrEmpty(input.Email) ? cliente.Email : input.Email;
                cliente.Contraseña = String.IsNullOrEmpty(input.Contraseña) ? cliente.Contraseña : input.Contraseña;
                cliente.Nombre = String.IsNullOrEmpty(input.Nombre) ? cliente.Nombre : input.Nombre;
                cliente.ApellidoPaterno = String.IsNullOrEmpty(input.ApellidoPaterno) ? cliente.ApellidoPaterno : input.ApellidoPaterno;
                cliente.ApellidoMaterno = String.IsNullOrEmpty(input.ApellidoMaterno) ? cliente.ApellidoMaterno : input.ApellidoMaterno;
                cliente.Direccion = String.IsNullOrEmpty(input.Direccion) ? cliente.Direccion : input.Direccion;
                _dbContext.SaveChanges();
                ClienteResponse response = new ClienteResponse
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Nombre = cliente.Nombre,
                    ApellidoPaterno = cliente.ApellidoPaterno,
                    ApellidoMaterno = cliente.ApellidoMaterno,
                    Direccion = cliente.Direccion,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                Cliente? cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null) { return NotFound(); }
                _dbContext.Clientes.Remove(cliente);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
