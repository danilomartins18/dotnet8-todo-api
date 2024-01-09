using AutoMapper;
using Microsoft.Extensions.Logging;
using ToDo.Application.Dtos.Requests;
using ToDo.Application.Dtos.Response;
using ToDo.Application.Interfaces.Repositories;
using ToDo.Application.Interfaces.Services;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services
{
	public class ToDoService(IMongoRepository<ToDoItem> repository, IMapper mapper, ILogger<ToDoService> logger) : IToDoService
	{
		private readonly IMongoRepository<ToDoItem> _repository = repository;
		private readonly IMapper _mapper = mapper;
		private readonly ILogger<ToDoService> _logger = logger;

		public async Task<AddToDoResponse> Add(AddToDoRequest request)
		{
			_logger.LogInformation("Adding a item: {Name}", request.Name);
			var todoItem = _mapper.Map<ToDoItem>(request);
			todoItem = await _repository.Add(todoItem);
			return _mapper.Map<AddToDoResponse>(todoItem);
		}

		public async Task<bool> Delete(string id)
		{
			_logger.LogInformation("Removing a item: {Id}", id);
			return await _repository.Remove(id);
		}

		public async Task<IEnumerable<ToDoItem>> GetAll()
		{
			_logger.LogInformation("Getting all items");
			return await _repository.GetAll();
		}

		public async Task<ToDoItem> GetById(string id)
		{
			_logger.LogInformation("Getting item: {Id}", id);
			return await _repository.GetById(id);
		}

		public async Task<bool> Update(UpdateToDoRequest request)
		{
			_logger.LogInformation("Updating item: {Id}", request.Id);
			var todoItem = _mapper.Map<ToDoItem>(request);
			return await _repository.Update(todoItem);
		}
	}
}
