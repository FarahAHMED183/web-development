namespace CRUD_Operation.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetBySpecification(IBaseSpecification<TEntity> specification)
        {
            var query = SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetFirstBySpecification(IBaseSpecification<TEntity> specification)
        {
            var query = SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<int> CountBySpecification(IBaseSpecification<TEntity> specification)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            
            // Apply only criteria for counting (no includes, ordering, or pagination)
            if (specification.Criterias != null && specification.Criterias.Any())
            {
                foreach (var criteria in specification.Criterias)
                {
                    query = query.Where(criteria);
                }
            }
            
            return await query.CountAsync();
        }
    

        public async Task<Global.PaginatedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize, IBaseSpecification<TEntity>? specification = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            var query = _context.Set<TEntity>().AsQueryable();

            // Apply specification criteria
            if (specification != null && specification.Criterias != null && specification.Criterias.Any())
            {
                foreach (var criteria in specification.Criterias)
                {
                    query = query.Where(criteria);
                }
            }

            // Includes
            if (specification != null && specification.Includes != null && specification.Includes.Any())
            {
                foreach (var include in specification.Includes)
                {
                    query = query.Include(include);
                }
            }

            // Ordering
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Global.PaginatedResult<TEntity>(items, pageNumber, pageSize, totalRecords, totalPages);
        }

    }
}