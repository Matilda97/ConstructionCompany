using ConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ConstructionCompany
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Инициализация БД путем выполнения кода в классе инициализатора с использование методов EF
            Database.SetInitializer(new ConstructionCompanyDbInitializer());

            //Инициализация БД путем запуска SQL инструкции из файла FillDB.sql
            //Database.SetInitializer(new ConstructionCompanyDbInitializer());

            using (var db = new ConstructionCompanyContext())
            {
                db.Database.Initialize(true);
            }
            //Database.SetInitializer<ConstructionCompanyContext>(new DropCreateDatabaseAlways<ConstructionCompanyContext>());
            //ConstructionCompanyContext A = new ConstructionCompanyContext();
            //A.Database.Initialize(true);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
