namespace ToDo.Application.Dtos.Requests
{
	public class AddToDoRequest
	{
        public string Name { get; set; } = string.Empty;
		public bool IsCompleted { get; set; }
	}
}
