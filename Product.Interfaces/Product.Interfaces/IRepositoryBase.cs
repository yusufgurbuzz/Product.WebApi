using System.Linq.Expressions;
using Product.Entity;

namespace Product.Interfaces;

public interface IRepositoryBase<T>
{
    IEnumerable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression,bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}