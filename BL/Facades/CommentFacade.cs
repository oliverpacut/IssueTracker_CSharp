using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;

namespace BL.Facades
{
    public class CommentFacade : AppFacadeBase
    {
        public CommentRepository Repository { get; set; }
        public CommentListQuery CommentListQuery { get; set; }

        protected IQuery<CommentDTO> CreateQuery(CommentFilter filter = null)
        {
            var query = CommentListQuery;
            query.Filter = filter;
            return query;
        }

        public void Create(CommentDTO dto)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(Mapper.Map<Comment>(dto));
                unitOfWork.Commit();
            }
        }
        public CommentDTO GetById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return Mapper.Map<CommentDTO>(Repository.GetById(id));
            }
        }

        public List<CommentDTO> GetAll()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(null).Execute().ToList();
            }
        }

        public List<CommentDTO> Get(CommentFilter filter)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(filter).Execute().ToList();
            }
        }

        public void UpdateComment(CommentDTO comment)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Update(Mapper.Map(comment, Repository.GetById(comment.Id)));
                unitOfWork.Commit();
            }
        }

        public void DeleteComment(CommentDTO comment)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(Mapper.Map<Comment>(comment));
                unitOfWork.Commit();
            }
        }

        public void DeleteIssueComments(int issueId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Get(new CommentFilter { IssueId = issueId })
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
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                ids.ToList().ForEach(y => Repository.Delete(y));
                unitOfWork.Commit();
            }
        }
    }
}
