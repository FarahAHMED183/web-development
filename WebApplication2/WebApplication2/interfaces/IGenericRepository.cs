namespace WebApplication2.interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    TEntity? GetById(int id);
    IEnumerable<TEntity> GetAll();
    IQueryable<TEntity> GetQueryable();
}
