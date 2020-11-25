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
    public class ProductsController : CheckAdmin
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var products = db.Products.Include(p => p.Category);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var links = from s in db.Products.Include(p=> p.Category)
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.NameProduct.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    links = links.OrderByDescending(s => s.NameProduct);
                    break;
                case "Date":
                    links = links.OrderBy(s => s.UnitPrice);
                    break;
                case "date_desc":
                    links = links.OrderByDescending(s => s.UnitPrice);
                    break;
                default:  // Name ascending 
                    links = links.OrderBy(s => s.NameProduct);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.IDCategory = new SelectList(db.Categories, "IDCategory", "NameCategory");
            Products pro = new Products();
            return View(pro);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Products pro)
        {
            try {
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Image = "~/Content/img/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                }
                db.Products.Add(pro);
                db.SaveChanges();
                ViewBag.IDCategory = new SelectList(db.Categories, "IDCategory", "NameCategory", pro.IDCategory);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pro);
            }
            
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCategory = new SelectList(db.Categories, "IDCategory", "NameCategory", products.IDCategory);
            return View(db.Products.Where(s => s.IDProduct == id).FirstOrDefault());
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, Products pro)
        {
            try
            {
                // TODO: Add update logic here
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName = fileName + extension;
                    pro.Image = "~/Content/img/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                }
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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

        public PartialViewResult detail()
        {
            return PartialView(db.Products.ToList());
        }

        //public Products GetCart()
        //{
        //    Products cart = Session["Cart"] as Products;
        //    if (cart == null || Session["Cart"] == null)
        //    {
        //        cart = new Products();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
        ////phương thúc add item vào giỏ hàng
        //public ActionResult AddtoCart(int id, int soluong =3)
        //{
        //    var pro = db.Products.SingleOrDefault(s => s.IDProduct == id);
        //    if (pro != null)
        //    {
        //        GetCart().Add(pro,soluong);
        //    }
        //    return RedirectToAction("Details", "Products");
        //}

    }
}
