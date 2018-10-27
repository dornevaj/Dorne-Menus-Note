
using System.Reflection;
using System.Web.Mvc;
using Evernote.BLL.Abstract;
using Evernote.BLL.Concrete;
using Evernote.DAL.Generic_Repository;
using Evernote.DAL.Unit_Of_Works;
using Evernote.Entities.DataBaseContext;
using global::SimpleInjector;
using global::SimpleInjector.Integration.Web;
using global::SimpleInjector.Integration.Web.Mvc;

namespace Evernote
{
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC Dependency Resolver.</summary>
        public static void Initialize()
        {
            // 1. Create a new Simple Injector container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // 3. Verify your configuration
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }

        //register our types, for instance
        private static void InitializeContainer(Container container)
        {
            container.Register<DataBaseContext>(Lifestyle.Scoped);

            Assembly[] assembliesGenericRepository = new[] { typeof(EfGenericRepository<>).Assembly };
            container.Register(typeof(IGenericRepository<>), assembliesGenericRepository);

            //Assembly[] assembliesUnitOfWorks = new[] { typeof(UnitOfWorks<>).Assembly };
            //container.Register(typeof(IUnitOfWorks<>), assembliesUnitOfWorks);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);


            container.Register<ICategoryService, CategoryManager>(Lifestyle.Scoped);
            container.Register<INoteService, NoteManager>(Lifestyle.Scoped);
            container.Register<IUserService, UserManager>(Lifestyle.Scoped);
            container.Register<IFilterLogService, FilterLogManager>(Lifestyle.Scoped);
            container.Register<ICommentService, CommentManager>(Lifestyle.Scoped);
            container.Register<ILikeService, LikeManager>(Lifestyle.Scoped);

        }
    }
}