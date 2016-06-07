using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;

namespace WebApp
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<BaseController>()
                    .WithServiceBase()
                    .WithServiceSelf()
                    .LifestyleTransient(),
                Component.For<IConfiguration>().Instance(Mapper.Configuration).LifestyleSingleton()
                    );

            container.ResolveAll<BaseController>().ToList().ForEach(x => x.RegisterMapping(Mapper.Configuration));
        }
    }
}