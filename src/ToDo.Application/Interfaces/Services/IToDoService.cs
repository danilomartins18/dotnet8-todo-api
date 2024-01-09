using ToDo.Application.Dtos.Requests;
using ToDo.Application.Dtos.Response;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces.Services
{
	public interface IToDoService
	{
		Task<IEnumerable<ToDoItem>> GetAll();
		Task<ToDoItem> GetById(string id);
		Task<AddToDoResponse> Add(AddToDoRequest request);
		Task<bool> Update(UpdateToDoRequest request);
		Task<bool> Delete(string id);
	}
}
