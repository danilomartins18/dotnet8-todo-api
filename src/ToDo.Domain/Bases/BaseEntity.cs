using MongoDB.Bson;

namespace ToDo.Domain.Bases
{
	public abstract class BaseEntity
	{
		public ObjectId Id { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
