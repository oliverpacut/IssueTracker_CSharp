using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;

namespace BL
{
    public class AppFacadeBase
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
    }
}
