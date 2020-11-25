using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TraiDoan.Models;
using PagedList;

namespace TraiDoan.Controllers
{
    public class blogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: blogs
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.namecate = sortOrder == "namecate" ?"namecate_desc": "namecate";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.blog.Include(p=> p.blogcate)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.dateblog);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.dateblog);
                    break;
                case "namecate":
                    students = students.OrderBy(s => s.blogcate.Nameblogcate);
                    break;
                case "namecate_desc":
                    students = students.OrderByDescending(s => s.blogcate.Nameblogcate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: blogs/Create
        public ActionResult Create()
        {
            ViewBag.IDblogcate = new SelectList(db.blogcate, "IDblogcate", "Nameblogcate");
            blog pro = new blog();
            return View(pro);
        }

        // POST: blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(blog pro)
        {
            try
            {
                if (pro.ImageUpload != null || pro.ImageUpload2 !=null || pro.ImageUpload3 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string fileName2 = Path.GetFileNameWithoutExtension(pro.ImageUpload2.FileName);
                    string fileName3 = Path.GetFileNameWithoutExtension(pro.ImageUpload3.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    string extension2 = Path.GetExtension(pro.ImageUpload2.FileName);
                    string extension3 = Path.GetExtension(pro.ImageUpload3.FileName);
                    fileName = fileName + extension;
                    fileName2 = fileName2 + extension2; 
                    fileName3 = fileName3 + extension3;
                    pro.img1 = "~/Content/img/" + fileName;
                    pro.img2 = "~/Content/img/" + fileName2;
                    pro.img3 = "~/Content/img/" + fileName3;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                    pro.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName2));
                    pro.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName3));
                }
                db.blog.Add(pro);
                db.SaveChanges();
                ViewBag.IDblogcate = new SelectList(db.blogcate, "IDblogcate", "Nameblogcate", pro.IDblogcate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pro);
            } 
        }

        // GET: blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDblogcate = new SelectList(db.blogcate, "IDblogcate", "Nameblogcate", blog.IDblogcate);
            return View(blog);
        }

        // POST: blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id,blog pro)
        {
            try
            {
                if (pro.ImageUpload != null || pro.ImageUpload2 != null || pro.ImageUpload3 != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string fileName2 = Path.GetFileNameWithoutExtension(pro.ImageUpload2.FileName);
                    string fileName3 = Path.GetFileNameWithoutExtension(pro.ImageUpload3.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    string extension2 = Path.GetExtension(pro.ImageUpload2.FileName);
                    string extension3 = Path.GetExtension(pro.ImageUpload3.FileName);
                    fileName = fileName + extension;
                    fileName2 = fileName2 + extension2;
                    fileName3 = fileName3 + extension3;
                    pro.img1 = "~/Content/img/" + fileName;
                    pro.img2 = "~/Content/img/" + fileName2;
                    pro.img3 = "~/Content/img/" + fileName3;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                    pro.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName2));
                    pro.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName3));
                }
                ViewBag.IDblogcate = new SelectList(db.blogcate, "IDblogcate", "Nameblogcate", pro.IDblogcate);
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            blog blog = db.blog.Find(id);
            db.blog.Remove(blog);
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
