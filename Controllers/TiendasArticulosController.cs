using Microsoft.AspNetCore.Mvc;
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TiendasArticulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendasArticulosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TiendasArticulosController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CrearTiendaArticuloDTO input)
        {
            try
            {
                Tienda? tienda = _dbContext.Tiendas.FirstOrDefault(t => t.Id == input.TiendaId);
                Articulo? articulo = _dbContext.Articulos.FirstOrDefault(a => a.Id == input.ArticuloId);
                if (tienda == null || articulo == null) { return NotFound(); }
                TiendaArticulo tiendaArticulo = new TiendaArticulo
                {
                    Tienda = tienda,
                    Articulo = articulo,
                    Fecha = DateTime.Now,
                };
                _dbContext.TiendasArticulos.Add(tiendaArticulo);
                _dbContext.SaveChanges();
                return Ok(tiendaArticulo);
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
                List<TiendaArticulo> tiendasArticulos = _dbContext
                    .TiendasArticulos
                    .Include(ta => ta.Tienda)
                    .Include(ta => ta.Articulo)
                    .ToList();
                return Ok(tiendasArticulos);
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
                TiendaArticulo? tiendaArticulo = _dbContext
                    .TiendasArticulos
                    .Include(ta => ta.Tienda)
                    .Include(ta => ta.Articulo)
                    .FirstOrDefault(ta => ta.Id == id);
                if (tiendaArticulo == null) { return NotFound(); }
                return Ok(tiendaArticulo);
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
                TiendaArticulo? tiendaArticulo = _dbContext.TiendasArticulos.FirstOrDefault(ta => ta.Id == id);
                if (tiendaArticulo == null) { return NotFound(); }
                _dbContext.TiendasArticulos.Remove(tiendaArticulo);
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
