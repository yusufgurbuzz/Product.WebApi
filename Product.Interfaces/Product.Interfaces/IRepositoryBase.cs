﻿using System.Linq.Expressions;

namespace Product.Interfaces;

public interface IRepositoryBase<T>
{
    IEnumerable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression,bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}