using Microsoft.AspNetCore.Mvc;

namespace Prueba_tecnica.Controllers
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController
    {


        private readonly data.AplicacionDb _context;
        public ProductosController(data.AplicacionDb context)
        {
            _context = context;
        }
        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.productoCategorias)
                .ThenInclude(pc => pc.Categoria)
                .ToListAsync();



            return Ok(productos);
        }
        [HttpPost]
        public async Task<ActionResult> PostProducto(ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var producto = new Models.producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            if (productoDto.Categorias != null && productoDto.Categorias.Any())
            {
                foreach (var categoriaId in productoDto.Categorias)
                {
                    var categoria = await _context.Categorias.FindAsync(categoriaId);
                    if (categoria != null)
                    {
                        var productoCategoria = new Models.ProductoCategoria
                        {
                            ProductoId = producto.Id,
                            CategoriaId = categoria.Id
                        };
                        _context.ProductoCategorias.Add(productoCategoria);
                    }
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetProductos), new { id = producto.Id }, producto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProducto(int id, ProductoDto productoDto)
        {
            if (id != productoDto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            producto.Nombre = productoDto.Nombre;
            producto.Descripcion = productoDto.Descripcion;
            producto.Precio = productoDto.Precio;
            producto.Stock = productoDto.Stock;
            _context.Entry(producto).State = EntityState.Modified;
            if (productoDto.Categorias != null && productoDto.Categorias.Any())
            {
                var existingCategorias = await _context.ProductoCategorias
                    .Where(pc => pc.ProductoId == id)
                    .ToListAsync();
                _context.ProductoCategorias.RemoveRange(existingCategorias);
                foreach (var categoriaId in productoDto.Categorias)
                {
                    var categoria = await _context.Categorias.FindAsync(categoriaId);
                    if (categoria != null)
                    {
                        var productoCategoria = new Models.ProductoCategoria
                        {
                            ProductoId = id,
                            CategoriaId = categoria.Id
                        };
                        _context.ProductoCategorias.Add(productoCategoria);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok(producto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return Ok(new {Message = "Producto eliminado Correctamente"});
        }
    }
}
public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public List<int> Categorias { get; set; } = new List<int>();
}

