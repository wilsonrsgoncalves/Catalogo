﻿using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;

    public AppDbContext _context = context;

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo ??=  new ProdutoRepository(_context);
        }
    }
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo ??= new CategoriaRepository(_context);
        }
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
