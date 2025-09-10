namespace CRUD_Operation.Repositories.Interfaces
{
    public interface IGenericRepository <TEntity> where TEntity : class
    {
        public Task Create(TEntity entity);

        public Task<List<TEntity>> GetAll();

        public Task<TEntity> GetById(int id);

        public Task Update(TEntity entity);

        public Task Delete(TEntity entity);

        // Specification methods
        public Task<List<TEntity>> GetBySpecification(IBaseSpecification<TEntity> specification);
        public Task<TEntity?> GetFirstBySpecification(IBaseSpecification<TEntity> specification);
        public Task<int> CountBySpecification(IBaseSpecification<TEntity> specification);
    }
}
