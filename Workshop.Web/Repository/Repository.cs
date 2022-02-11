using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Workshop.Web.Models;

namespace Workshop.Web.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SchoolDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(SchoolDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }
    }
}
