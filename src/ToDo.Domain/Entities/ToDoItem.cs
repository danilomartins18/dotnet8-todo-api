using ToDo.Domain.Attributes;
using ToDo.Domain.Bases;

namespace ToDo.Domain.Entities
{
	[BsonCollection("item")]
	public class ToDoItem : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public bool IsCompleted { get; set; }
	}
}
