using ToDo.Domain.Bases;

namespace ToDo.Application.Interfaces.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
		Task<T> InsertAsync(T item);
		Task<T> UpdateAsync(T item);
		Task<bool> DeleteAsync(Guid id);
		Task<T> SelectAsync(Guid id);
		Task<IEnumerable<T>> SelectAsync();
		Task<bool> ExistAsync(Guid id);
	}
}
