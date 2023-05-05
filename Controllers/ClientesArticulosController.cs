using Microsoft.AspNetCore.Mvc;
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClientesArticulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesArticulosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ClientesArticulosController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CrearClienteArticuloDTO input)
        {
            try
            {
                Cliente? cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == input.ClienteId);
                Articulo? articulo = _dbContext.Articulos.FirstOrDefault(a => a.Id == input.ArticuloId);
                if (cliente == null || articulo == null) { return NotFound(); }
                ClienteArticulo clienteArticulo = new ClienteArticulo
                {
                    Cliente = cliente,
                    Articulo = articulo,
                    Fecha = DateTime.Now,
                };
                _dbContext.ClientesArticulos.Add(clienteArticulo);
                _dbContext.SaveChanges();
                ClienteArticuloResponse response = new ClienteArticuloResponse
                {
                    Id = clienteArticulo.Id,
                    Cliente = new ClienteResponse
                    {
                        Id = clienteArticulo.Cliente.Id,
                        Email = clienteArticulo.Cliente.Email,
                        Nombre = clienteArticulo.Cliente.Nombre,
                        ApellidoPaterno = clienteArticulo.Cliente.ApellidoPaterno,
                        ApellidoMaterno = clienteArticulo.Cliente.ApellidoMaterno,
                        Direccion = clienteArticulo.Cliente.Direccion,
                    },
                    Articulo = clienteArticulo.Articulo,
                    Fecha = clienteArticulo.Fecha,
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
                List<ClienteArticulo> clientesArticulos = _dbContext
                    .ClientesArticulos
                    .Include(ca => ca.Cliente)
                    .Include(ca => ca.Articulo)
                    .ToList();

                List<ClienteArticuloResponse> response = clientesArticulos
                    .Select(ca => new ClienteArticuloResponse
                    {
                        Id = ca.Id,
                        Cliente = new ClienteResponse
                        {
                            Id = ca.Cliente.Id,
                            Email = ca.Cliente.Email,
                            Nombre = ca.Cliente.Nombre,
                            ApellidoPaterno = ca.Cliente.ApellidoPaterno,
                            ApellidoMaterno = ca.Cliente.ApellidoMaterno,
                            Direccion = ca.Cliente.Direccion,
                        },
                        Articulo = ca.Articulo,
                        Fecha = ca.Fecha,
                    })
                    .ToList();
                return Ok(response);
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
                ClienteArticulo? clienteArticulo = _dbContext
                    .ClientesArticulos
                    .Include(ca => ca.Cliente)
                    .Include(ca => ca.Articulo)
                    .FirstOrDefault(ca => ca.Id == id);
                if (clienteArticulo == null) { return NotFound(); }
                ClienteArticuloResponse response = new ClienteArticuloResponse
                {
                    Id = clienteArticulo.Id,
                    Cliente = new ClienteResponse
                    {
                        Id = clienteArticulo.Cliente.Id,
                        Email = clienteArticulo.Cliente.Email,
                        Nombre = clienteArticulo.Cliente.Nombre,
                        ApellidoPaterno = clienteArticulo.Cliente.ApellidoPaterno,
                        ApellidoMaterno = clienteArticulo.Cliente.ApellidoMaterno,
                        Direccion = clienteArticulo.Cliente.Direccion,
                    },
                    Articulo = clienteArticulo.Articulo,
                    Fecha = clienteArticulo.Fecha,
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
                ClienteArticulo? clienteArticulo = _dbContext.ClientesArticulos.FirstOrDefault(ca => ca.Id == id);
                if (clienteArticulo == null) { return NotFound(); }
                _dbContext.ClientesArticulos.Remove(clienteArticulo);
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
