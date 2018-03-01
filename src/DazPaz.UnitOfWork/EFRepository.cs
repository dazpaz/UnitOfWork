using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DazPaz.UnitOfWork
{
	public class EFRepository<T> : IRepository<T> where T : class
	{
		protected DbContext DbContext { get; set; }
		protected DbSet<T> DbSet { get; set; }

		public EFRepository(DbContext dbContext)
		{
			DbContext = dbContext ?? throw new ArgumentNullException("dbContext");
			DbSet = DbContext.Set<T>();
		}

		public virtual IQueryable<T> GetAll()
		{
			return DbSet;
		}

		public virtual T GetById(int id)
		{
			return DbSet.Find(id);
		}

		public void Add(T entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State != EntityState.Detached)
			{
				dbEntityEntry.State = EntityState.Added;
			}
			else
			{
				DbSet.Add(entity);
			}
		}

		public void Update(T entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}
			dbEntityEntry.State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
			if (dbEntityEntry.State != EntityState.Deleted)
			{
				dbEntityEntry.State = EntityState.Deleted;
			}
			else
			{
				DbSet.Attach(entity);
				DbSet.Remove(entity);
			}
		}

		public void Delete(int id)
		{
			var entity = GetById(id);
			if (entity == null) return;
			Delete(entity);
		}
	}
}
