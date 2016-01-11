using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Asp_Mvc_2015_2016.DAL;
using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.DAL.Services;
//using Asp_Mvc_2015_2016.Models.DAL;

namespace Asp_Mvc_2015_2016
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<FacturatieDBContext, FacturatieDBContext>(new PerResolveLifetimeManager());
            
            container.RegisterType<IGebruikerService, GebruikerService>();
            container.RegisterType<IUurRegistratieService, UurRegistratieService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}