namespace ToDo.Application.Dtos.Requests
{
	public class UpdateToDoRequest : AddToDoRequest
	{
        public string Id { get; set; } = string.Empty;
	}
}
