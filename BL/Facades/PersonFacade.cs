using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.Identity;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using BL.DTO.Filters;
using BL.Queries;
using BL.Repositories;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace BL.Facades
{
    public class PersonFacade : AppFacadeBase
    {
        public PersonRepository Repository { get; set; }
        public PersonListQuery personListQuery { get; set; }

        protected IQuery<PersonDTO> CreateQuery(PersonFilter filter = null)
        {
            var query = personListQuery;
            query.Filter = filter;
            return query;
        }

        public void CreatePerson(PersonDTO person)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Insert(Mapper.Map<Person>(person));
                unitOfWork.Commit();
            }
        }

        public PersonDTO GetPersonById(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return Mapper.Map<PersonDTO>(Repository.GetById(id));
            }
        }

        public List<PersonDTO> GetAllPeople()
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(null).Execute().ToList();
            }
        }

        public List<PersonDTO> GetPeople(PersonFilter filter)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                return CreateQuery(filter).Execute().ToList();
            }
        }

        public void UpdatePerson(PersonDTO person)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Update(Mapper.Map(person, Repository.GetById(person.Id)));
                unitOfWork.Commit();
            }
        }

        public void DeletePerson(PersonDTO person)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                Repository.Delete(Mapper.Map<Person>(person));
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

