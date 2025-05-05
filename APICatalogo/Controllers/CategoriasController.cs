using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            if (_context.Categorias  == null)
            {
                return NotFound("No categories available.");
            }

            return await _context.Categorias
                .Include(c => c.Produtos)
                .Where(c => c.CategoriaId <= 5)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            if (_context.Categorias == null)
            {
                return NotFound("No categories available.");
            }

            return await _context.Categorias
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound("No categories available.");
            }

            var categoria = await _context.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada...");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Categoria não pode conter o valor null.");
            }

            if (_context.Categorias == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            if (categoria == null || id != categoria.CategoriaId)
            {
                return BadRequest("Invalid categoria or ID mismatch.");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (_context.Categorias == null)
            {
                return NotFound("No categories available.");
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(p => p.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada...");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}
