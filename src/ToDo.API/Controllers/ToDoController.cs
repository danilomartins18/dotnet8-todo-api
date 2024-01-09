using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Dtos.Requests;
using ToDo.Application.Interfaces.Services;
using ToDo.Domain.Entities;

namespace ToDo.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ToDoController(IToDoService todoService) : ControllerBase
	{
		private readonly IToDoService _todoService = todoService;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
		{
			var todoItems = await _todoService.GetAll();
			return Ok(todoItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ToDoItem>> GetById(string id)
		{
			var todoItem = await _todoService.GetById(id);
			if (todoItem == null)
			{
				return NotFound();
			}
			return Ok(todoItem);
		}

		[HttpPost]
		public async Task<ActionResult<ToDoItem>> Add(string title)
		{
			var createdItem = await _todoService.Add(new AddToDoRequest { Name = title, IsCompleted = false });
			return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
		}

		[HttpPut()]
		public async Task<IActionResult> Update(UpdateToDoRequest request)
		{
			var isUpdated = await _todoService.Update(request);
			if (!isUpdated)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var isDeleted = await _todoService.Delete(id);
			if (!isDeleted)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}