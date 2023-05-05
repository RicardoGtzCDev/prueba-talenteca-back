using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using back.Models.Context;
using Microsoft.AspNetCore.Authorization;

namespace Articulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ArticulosController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CrearArticuloDTO input)
        {
            try
            {
                Articulo articulo = new Articulo
                {
                    Codigo = input.Codigo,
                    Descripcion = input.Descripcion,
                    Precio = input.Precio,
                    Imagen = input.Imagen,
                    Stock = input.Stock,
                };
                _dbContext.Articulos.Add(articulo);
                _dbContext.SaveChanges();
                return Ok(articulo);
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
                List<Articulo> articulos = _dbContext.Articulos.ToList();
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("id")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                Articulo? articulo = _dbContext.Articulos.FirstOrDefault(a => a.Id == id);
                if (articulo == null) { return NotFound(); }
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("id")]
        [Authorize]
        public IActionResult Patch(int id, ActualizarArticuloDTO input)
        {
            try
            {
                Articulo? articulo = _dbContext.Articulos.FirstOrDefault(a => a.Id == id);
                if (articulo == null) { return NotFound(); }
                articulo.Codigo = String.IsNullOrEmpty(input.Codigo) ? articulo.Codigo : input.Codigo;
                articulo.Descripcion = String.IsNullOrEmpty(input.Descripcion) ? articulo.Descripcion : input.Descripcion;
                if (input.Precio != null) {articulo.Precio = (decimal)input.Precio; }
                articulo.Imagen = String.IsNullOrEmpty(input.Imagen) ? articulo.Imagen : input.Imagen;
                if (input.Stock != null) {articulo.Stock = (int)input.Stock; }
                _dbContext.SaveChanges();
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("id")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                Articulo? articulo = _dbContext.Articulos.FirstOrDefault(a => a.Id == id);
                if (articulo == null) { return NotFound(); }
                _dbContext.Articulos.Remove(articulo);
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
