﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product.Interfaces;

namespace Product.Repositories;

public class RepositoryBase <T> : IRepositoryBase<T> where T: class
{
    protected readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }
        
    
    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}