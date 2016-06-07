using AutoMapper;
using BL.DTO;
using DAL.Entities;

namespace BL
{
    public class Mapping
    {
        
        public static void Create()
        {
            Mapper.CreateMap<Issue, IssueDTO>();
            Mapper.CreateMap<IssueDTO, Issue>();

            Mapper.CreateMap<Project, ProjectDTO>();
            Mapper.CreateMap<ProjectDTO, Project>();

            Mapper.CreateMap<ApplicationUserDTO, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserDTO>();

            Mapper.CreateMap<ApplicationUserDTO, PersonDTO>();
            Mapper.CreateMap<PersonDTO, ApplicationUserDTO>();

            Mapper.CreateMap<Person, PersonDTO>();
            Mapper.CreateMap<PersonDTO, Person>();

            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<CommentDTO, Comment>();

            Mapper.CreateMap<Notification, NotificationDTO>();
            Mapper.CreateMap<NotificationDTO, Notification>();
        }
        
    }
}
