using AutoMapper;
using TaskManagement.Web.Models;
using TaskManagment.Domain;

namespace TaskManagement.Web.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskEntity, TaskModel>()
                .ReverseMap();
        }
    }
}
