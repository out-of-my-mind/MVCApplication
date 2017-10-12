using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

namespace MVCApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region 数据库初始化，重新创建数据库  或者  当模型变化时重建数据库
            //          DropCreateDatabaseAlways  DropCreateDatabaseIfModelChanges
            //需要一个泛型类型的参数，并且是DbContext的派生类
            Database.SetInitializer(new MVCApplication.Models.MusicStoreDbInitializer()
                /*new DropCreateDatabaseAlways<MVCApplication.Models.MVCMusicStoreDB>()*/  );
            #endregion

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
