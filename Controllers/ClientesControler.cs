using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back.Models.Context;

namespace back.Controllers
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
        public IActionResult Post(CrearClienteDTO input)
        {
            try
            {
                Cliente cliente = new Cliente()
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
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Cliente> clientes = _dbContext.Clientes.ToList();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            try
            {
                Cliente? cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == id);
                if (cliente == null) { return NotFound(); }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("id")]
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
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("id")]
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
