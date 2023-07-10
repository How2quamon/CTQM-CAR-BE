using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<List<TEntity>> GetAll();
		Task<TEntity> GetById(Guid id);
		Task<TEntity> Add(TEntity entity);
		TEntity Update(TEntity entity);
		Task<bool> Delete(Guid id);
	}
}
