using BL.Identity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Data.Entity;

namespace BL
{
    public class DataInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Component.For<Func<DbContext>>()
                    .Instance(() => new AppDbContext())
                    .LifestyleTransient(),

                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<AppUnitOfWorkProvider>()
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkRegistry>()
                    .Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
                    .LifestyleSingleton(),

                Classes.FromAssemblyContaining<AppUnitOfWork>()
                    .BasedOn(typeof(AppQuery<>))
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<AppUnitOfWork>()
                    .BasedOn(typeof(IRepository<,>))
                    .LifestyleTransient(),

                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<,>))
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<AppFacadeBase>()
                    .BasedOn<AppFacadeBase>()
                    .LifestyleTransient(),

                Component.For<ApplicationUserManager>()
                    .LifestyleTransient(),

                Component.For<Func<ApplicationUserManager>>()
                    .Instance(container.Resolve<ApplicationUserManager>)
                    .LifestyleTransient(),

                Component.For<IUserStore<ApplicationUser, int>>()
                    .ImplementedBy<ApplicationUserStore>()
                    .LifestyleTransient()
            );

            container.Resolve<IUnitOfWorkProvider>();

            Mapping.Create();

        }
    }
}
