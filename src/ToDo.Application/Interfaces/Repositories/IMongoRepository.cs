using System.Linq.Expressions;
using ToDo.Domain.Bases;

namespace ToDo.Application.Interfaces.Repositories
{
	public interface IMongoRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(string id);
		Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter);
		Task<T> Add(T entity);
		Task<bool> Update(T entity);
		Task<bool> Remove(string id);
	}
}
