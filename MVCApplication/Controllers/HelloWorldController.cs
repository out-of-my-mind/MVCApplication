using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld  ActionResult
        public ActionResult Index()
        {
            //"This is <b>Defult</b> action";
            return View();
        }

        //public string Welcome(string name,int id = 1) {
        //    //使用编码输出，避免恶意javascript输入
        //    return HttpUtility.HtmlEncode("This is the Welcome action method. Wecome " + name + ",timenum is "+ id);
        //}

        public ActionResult Welcome(string name, int id = 1) {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = id;
            return View();
        }
    }
}