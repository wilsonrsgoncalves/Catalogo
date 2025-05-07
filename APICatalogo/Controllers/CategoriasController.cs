using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController(ICategoriaRepository repository,
    ILogger<CategoriasController> logger) : ControllerBase
{
    private readonly ICategoriaRepository _repository = repository;
    private readonly ILogger<CategoriasController> _logger = logger;

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _repository.GetAll();
        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            const string logMessageTemplate = "Categoria com id={Id} não encontrada...";
            _logger.LogWarning(logMessageTemplate, id);
            return NotFound($"Categoria com id={id} não encontrada...");
        }
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            const string logMessageTemplate = "Dados inválidos...";
            _logger.LogWarning(logMessageTemplate);
            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.CategoriaId },
            categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            const string logMessageTemplate = "Dados inválidos...";
            _logger.LogWarning(logMessageTemplate);
            return BadRequest("Dados inválidos");
        }

        _repository.Update(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            const string logMessageTemplate = "Categoria com id={Id} não encontrada...";
            _logger.LogWarning(logMessageTemplate, id);
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _repository.Delete(categoria);
        return Ok(categoriaExcluida);
    }
}