using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TraiDoan.Models;

namespace TraiDoan.Controllers
{
    public class blog_categoryController : CheckAdmin
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: blog_category
        public ActionResult Index()
        {
            return View(db.blogcate.ToList());
        }

        // GET: blog_category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blogcate.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // GET: blog_category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: blog_category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDblogcate,Nameblogcate")] blog_category blog_category)
        {
            if (ModelState.IsValid)
            {
                db.blogcate.Add(blog_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog_category);
        }

        // GET: blog_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blogcate.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // POST: blog_category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDblogcate,Nameblogcate")] blog_category blog_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog_category);
        }

        // GET: blog_category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blogcate.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // POST: blog_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            blog_category blog_category = db.blogcate.Find(id);
            db.blogcate.Remove(blog_category);
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
