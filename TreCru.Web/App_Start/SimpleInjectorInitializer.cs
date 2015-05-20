using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using TreCru.Web.App_Start;
using TreCru.Web.Entity;
using TreCru.Web.Services;
using TreCru.Web.Services.Interfaces;
using WebActivator;

[assembly: PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace TreCru.Web.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();

            RegisterEntities(container);
            RegisterServices(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterEntities(Container container)
        {
            container.RegisterPerWebRequest<TreasureCruiseEntities>();

            var contextName = typeof(TreasureCruiseEntities).Name;

            var entities =
                Assembly.GetExecutingAssembly()
                    .DefinedTypes.Where(
                        x => x.Namespace == typeof(TreasureCruiseEntities).Namespace && x.Name != contextName);

            var repoInterface = typeof(IRepository<>);
            var repoClass = typeof(Repository<,>);

            foreach (var entity in entities)
            {
                var genericInterface = repoInterface.MakeGenericType(entity);
                var genericClass = repoClass.MakeGenericType(typeof(TreasureCruiseEntities), entity);

                container.Register(genericInterface, genericClass, Lifestyle.Singleton);
            }
        }

        private static void RegisterServices(Container container)
        {
            // ...
        }
    }
}