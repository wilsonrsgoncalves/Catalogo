using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
{
    private readonly AppDbContext _context = context;

    public IEnumerable<Categoria> GetCategorias()
    {   var categorias = _context.Categorias.ToList();
        if (categorias is not null)
        {
            return categorias;
        }
        throw new InvalidOperationException("Nenhuma categoria encontrada");
    }
    public Categoria GetCategoria(int id)
    {
        var categorias = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        
        if (categorias is not null)
            return categorias;

        throw new InvalidOperationException("Nenhuma categoria encontrada");
    }
    public Categoria Create(Categoria categoria)
    {
        if (categoria is not null)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }

        throw new ArgumentNullException(nameof(categoria));
    }
    public Categoria Update(Categoria categoria)
    {
        if (categoria is not null)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return categoria;
        }
        throw new ArgumentNullException(nameof(categoria));
    }

    public Categoria Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);
        if (categoria is not null)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return categoria;
        }
        throw new InvalidOperationException("Categoria não encontrada");
    }
}
