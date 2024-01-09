using AutoMapper;
using MongoDB.Bson;
using ToDo.Application.Dtos.Requests;
using ToDo.Application.Dtos.Response;
using ToDo.Domain.Entities;

namespace ToDo.Application.AutoMapper
{
	public class TodoMappingProfile : Profile
	{
		public TodoMappingProfile()
		{
			CreateMap<AddToDoRequest, ToDoItem>();
			CreateMap<ToDoItem, AddToDoResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
			CreateMap<UpdateToDoRequest, ToDoItem> ()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

		}
	}
}
