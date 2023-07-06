using CTQM_CAR.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public Repository(DbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public async Task<List<TEntity>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<TEntity> GetById(string id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<TEntity> Add(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
			return entity;
		}

		public TEntity Update(TEntity entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			return entity;
		}

		public async Task<bool> Delete(string id)
		{
			try
			{
				TEntity entity = await _dbSet.FindAsync(id);
				_dbSet.Remove(entity);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}
	}
}
