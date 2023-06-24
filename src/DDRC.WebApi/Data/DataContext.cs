using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace DDRC.WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public bool IsAvailable()
        {
            return Database.CanConnect();
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public EntityEntry<TEntity> GetDbEntry<TEntity>(TEntity data) where TEntity : class
        {
            return Entry(data);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Set<TEntity>().AsQueryable();
        }

        public void AddData<TEntity>(TEntity data) where TEntity : class
        {
            Add(data);
        }

        public void UpdateData<TEntity>(TEntity data) where TEntity : class
        {
            Update(data);
        }

        public void DeleteData<TEntity>(TEntity data) where TEntity : class
        {
            Remove(data);
        }

        public int CommitChanges()
        {
            return SaveChanges();
        }

        public void RollBackChanges()
        {
            Database.RollbackTransaction();
        }
    }
}
