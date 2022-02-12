using System.Linq.Expressions;

namespace Workshop.Web.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetById(int id);

        Task Create(TEntity entity);


        void Update(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> FilterByExpression(Expression<Func<TEntity, bool>> filter);
    }
}
