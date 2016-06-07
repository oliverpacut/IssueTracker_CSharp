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
    public class IssueFacade : AppFacadeBase
    {
        public IssueRepository Repository { get; set; }
        public IssueListQuery IssueListQuery { get; set; }
        
        
        protected IQuery<IssueDTO> CreateQuery(IssueFilter filter = null)
        {
            var query = IssueListQuery;
            query.Filter = filter;
            return query;
        }

        public void CreateIssue(IssueDTO issue)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(Mapper.Map<Issue>(issue));
                unitOfWork.Commit();
            }
        }

        public IssueDTO GetIssueById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return Mapper.Map<IssueDTO>(Repository.GetById(id));
            }
        }

        public List<IssueDTO> GetAllIssues()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(null).Execute().ToList();
            }
        }

        public List<IssueDTO> GetIssues(IssueFilter filter)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(filter).Execute().ToList();
            }
        }

        public void UpdateIssue(IssueDTO issue)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Update(Mapper.Map(issue, Repository.GetById(issue.Id)));
                unitOfWork.Commit();
            }
        }

        public void DeleteIssue(IssueDTO issue)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(Mapper.Map<Issue>(issue));
                unitOfWork.Commit();
            }
        }

        public void DeleteProjectIssues(int projectId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                GetIssues(new IssueFilter { ProjectId = projectId })
                    .ForEach(y => Repository.Delete(y.Id));
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
            ids.ToList().ForEach(y => DeleteById(y));
        }
    }
}
