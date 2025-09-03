using WebApplication2.Data;
using WebApplication2.interfaces;

namespace WebApplication2.Services;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }
    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>().AsQueryable();
    }
}
