using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context) => _context = context;

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {

        if (_context.Produtos == null)
        {
            return NotFound("Produto não encontrado.");
        }

        var produtos = _context.Produtos.ToList();
        if (produtos is null)
        {
            return NotFound();
        }
        return produtos;
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        if (_context.Produtos == null)
        {
            return NotFound("Produto não encontrado.");
        }

        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        if (_context.Produtos == null)
        {
            return NotFound("Produto não encontrado.");
        }

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (_context.Produtos == null)
        {
            return NotFound("Nenhum produto disponível no banco de dados.");
        }

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto == null)
        {
            return NotFound("Produto não encontrado.");
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return Ok(produto);
    }
}