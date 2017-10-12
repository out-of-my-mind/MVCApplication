using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCApplication.Models;

namespace MVCApplication.Controllers
{
    public class MVCMoviesController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();
        // GET: MVCMovies
        public ActionResult Index(string searchstring)
        {
            var movies = from m in db.Movies select m;//LINQ查询
            if (!String.IsNullOrEmpty(searchstring))
            {
                movies = movies.Where(o => o.Title.Contains(searchstring));//o=>o   Lambda表达式，基于LINQ查询
            }
            return View(movies);
        }

        //[HttpPost]
        //public ActionResult Index(FormCollection fc, string id)
        //{
        //    string searchstring = id;
        //    var movies = from m in db.Movies select m;//LINQ查询
        //    if (!String.IsNullOrEmpty(searchstring))
        //    {
        //        movies = movies.Where(o => o.Title.Contains(searchstring));//o=>o   Lambda表达式，基于LINQ查询
        //    }
        //    return View(movies);
        //}


        // GET: MVCMovies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCMovie mVCMovie = db.Movies.Find(id);
            if (mVCMovie == null)
            {
                return HttpNotFound();
            }
            return View(mVCMovie);
        }

        // GET: MVCMovies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVCMovies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MVCMovie mVCMovie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(mVCMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mVCMovie);
        }

        // GET: MVCMovies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCMovie mVCMovie = db.Movies.Find(id);
            if (mVCMovie == null)
            {
                return HttpNotFound();
            }
            return View(mVCMovie);
        }

        // POST: MVCMovies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] MVCMovie mVCMovie)
        {
            if (ModelState.IsValid)//表单数据是否可用于修改（编辑或更新）一个 Movie 对象
            {
                db.Entry(mVCMovie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mVCMovie);
        }

        // GET: MVCMovies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCMovie mVCMovie = db.Movies.Find(id);
            if (mVCMovie == null)
            {
                return HttpNotFound();
            }
            return View(mVCMovie);
        }

        // POST: MVCMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MVCMovie mVCMovie = db.Movies.Find(id);
            db.Movies.Remove(mVCMovie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
