using AutoMapper;
using ToDoSecure.DTO;
using ToDoSecure.Models;

namespace ToDoSecure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItems, ToDoDTO>().ReverseMap();
        }
    }
}
