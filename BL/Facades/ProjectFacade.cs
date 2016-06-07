using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;

namespace BL.Facades
{
    public class ProjectFacade : AppFacadeBase
    {
        public ProjectRepository Repository { get; set; }
        public ProjectListQuery ProjectListQuery { get; set; }
        

        protected IQuery<ProjectDTO> CreateQuery(ProjectFilter filter = null)
        {
            var query = ProjectListQuery;
            query.Filter = filter;
            return query;
        }

        public void CreateProject(ProjectDTO project)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(Mapper.Map<Project>(project));
                unitOfWork.Commit();
            }
        }

        public ProjectDTO GetProjectById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return Mapper.Map<ProjectDTO>(Repository.GetById(id));
            }
        }
        public List<ProjectDTO> GetAllProjects()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(null).Execute().ToList();
            }
        }

        public List<ProjectDTO> GetProjects(ProjectFilter filter)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(filter).Execute().ToList();
            }
        }

        public void UpdateProject(ProjectDTO project)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Update(Mapper.Map(project, Repository.GetById(project.Id)));
                unitOfWork.Commit();
            }
        }

        public void DeleteProject(ProjectDTO project)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(Mapper.Map<Project>(project));
                unitOfWork.Commit();
            }
        }

        public void DeleteById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(id);
                unitOfWork.Commit();
            }
        }

        public void DeleteByIds(params int[] ids)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                ids.ToList().ForEach(y => Repository.Delete(y));
                unitOfWork.Commit();
            }
        }
    }
}
