using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;

namespace Tiendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TiendasController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CrearTiendaDTO input)
        {
            try
            {
                Tienda tienda = new Tienda
                {
                    Sucursal = input.Sucursal,
                    Direccion = input.Direccion,
                };
                _dbContext.Tiendas.Add(tienda);
                _dbContext.SaveChanges();
                return Ok(tienda);
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
                List<Tienda> tiendas = _dbContext.Tiendas.ToList();
                return Ok(tiendas);
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
                Tienda? tienda = _dbContext.Tiendas.FirstOrDefault(t => t.Id == id);
                if (tienda == null) { return NotFound(); }
                return Ok(tienda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult Patch(int id, ActualizarTiendaDTO input)
        {
            try
            {
                Tienda? tienda = _dbContext.Tiendas.FirstOrDefault(t => t.Id == id);
                if (tienda == null) { return NotFound(); }
                tienda.Sucursal = String.IsNullOrEmpty(input.Sucursal) ? tienda.Sucursal : input.Sucursal;
                tienda.Direccion = String.IsNullOrEmpty(input.Direccion) ? tienda.Direccion : input.Direccion;                
                _dbContext.SaveChanges();
                return Ok(tienda);
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
                Tienda? tienda = _dbContext.Tiendas.FirstOrDefault(t => t.Id == id);
                if (tienda == null) { return NotFound(); }
                _dbContext.Tiendas.Remove(tienda);
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
