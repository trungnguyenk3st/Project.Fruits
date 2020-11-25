using PagedList;
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

namespace TraiDoan.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Home
        
        public ActionResult Index()
        {         
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }
        public ActionResult HOmeTestsearch()
        {
            return View();
        }

        public ActionResult Products(int? minamount, int? maxamount, int? page) // product + productdefault + searchproduct 
        {
            ViewBag.NameSortParm = minamount;
            ViewBag.DateSortParm = maxamount;
            if (page == null) page = 1;
            var links = (from l in db.Products select l);
            if (minamount == null || maxamount == null)
            {
                minamount = 1;
                maxamount = 1000;
                links = links.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderBy(s => s.NameProduct).Select(s => s);
            }               
            links = links.OrderBy(s => s.IDProduct).Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderBy(s => s.IDProduct);
            int pagesize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pagesize));
        }
        public PartialViewResult productdefault() 
        {
            var products = db.Products.Include(p => p.Category);
            return PartialView(products);
        }

        public PartialViewResult searchproduct(int? minamount, int? maxamount, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.Mina = minamount;
            ViewBag.Maxa = maxamount;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                ViewBag.Mina = minamount;   
                ViewBag.Maxa = maxamount;
            }

            ViewBag.CurrentFilter = searchString;


            var students = from s in db.Products
                           select s;
            if (minamount == null || maxamount == null)
            {
                minamount = 1;
                maxamount = 1000;
                students = students.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderBy(s => s.NameProduct).Select(s => s);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.NameProduct.StartsWith(searchString) && s.UnitPrice < maxamount && s.UnitPrice > minamount);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderByDescending(s => s.NameProduct);
                    break;
                case "price":
                    students = students.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderBy(s => s.UnitPrice);
                    break;
                case "price_desc":
                    students = students.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderByDescending(s => s.UnitPrice);
                    break;
                default:  // Name ascending 
                    students = students.Where(s => s.UnitPrice < maxamount && s.UnitPrice > minamount).OrderBy(s => s.NameProduct);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return PartialView(students.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult parProDefault (int? id) // chia sản phẩm trong category 
        {
            Category cate = db.Categories.Find(id);
            ViewBag.id = cate.IDCategory;
            ViewBag.name = cate.NameCategory;
            var products = db.Products.Include(p => p.Category);           
            return PartialView(products.ToList());
        }
        public PartialViewResult categoryPartial() //danh sách category sử dụng ở home
        {
            var cate = db.Categories.ToList();
            return PartialView(cate);
        }

        public PartialViewResult testPage()
        {
            try
            {
                var hangnhat = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 1
                var hangnhi = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).Where(p => p.IDProduct != hangnhat).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 2
                var hangba = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).Where(p => p.IDProduct != hangnhat && p.IDProduct != hangnhi).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 3
                ViewBag.st1 = hangnhat;
                ViewBag.th2 = hangnhi;
                ViewBag.rd3 = hangba;
                //var hotnhat = db.OrderDetails.Where(s => s.Order.IDorder.Equals(s.IDOrder) && s.IDProduct.Equals(hangnhat)).ToList();
                var HotProduct = db.Products.Include(s => s.OrderDetails);
                return PartialView(HotProduct);
            }
            catch
            {
                var HotProduct = db.Products.Include(s => s.OrderDetails);
                return PartialView(HotProduct);
            }
        }
        public PartialViewResult Homesearch(string search, string currentFilter, int? page) //Thanh search ở home
        {
            var products = db.Products.Include(p => p.Category);
            
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;
            ViewBag.erro = search;
            
            products = from s in db.Products.Include(p => p.Category) select s;

            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(s => s.NameProduct.Contains(search));
                ViewBag.count = db.Products.Where(s => s.NameProduct.Contains(search)).Count();
            }

            products = products.OrderByDescending(s => s.NameProduct.Contains(search));

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Contacts()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }
        public PartialViewResult Blog()
        {
            var blog = db.blog.Include(p => p.blogcate);
            return PartialView(blog.ToList());
        }
        public PartialViewResult blog1()
        {
            var blog = db.blog.Include(p => p.blogcate);
            return PartialView(blog.ToList());
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");//mehthod login là Index
        }


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

        public ActionResult Blog_Details(int? id)
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

        public PartialViewResult Blog_Cate(int? id)
        {
            var cate = db.blogcate.ToList();
            return PartialView(cate);
        }

        public PartialViewResult Blog_ParCate(int? id)
        {
            blog_category cate = db.blogcate.Find(id);
            ViewBag.id = cate.IDblogcate;
            ViewBag.name = cate.Nameblogcate;
            var product = db.blog.Include(s => s.blogcate);
            return PartialView(product.ToList());
        }

        public PartialViewResult LatestProduct()
        {
            //var LatestProduct = db.Products.OrderByDescending(s => s.IDProduct).Take(3).ToList();  mới nhất
            var LatestProduct = db.Products.OrderBy(s => s.IDProduct).Take(3).ToList(); /*cũ nhất*/
            return PartialView(LatestProduct);
        }
        public PartialViewResult NewProduct()
        {
            var NewProduct = db.Products.OrderByDescending(s => s.IDProduct).Take(3).ToList();  /*mới nhất*/
            /*var LatestProduct = db.Products.OrderBy(s => s.IDProduct).Take(3).ToList();*/  /*cũ nhất*/
            return PartialView(NewProduct);
        }
        public PartialViewResult HotProduct()
        {
            try
            {
                var hangnhat = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 1
                var hangnhi = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).Where(p => p.IDProduct != hangnhat).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 2
                var hangba = Convert.ToInt32(db.OrderDetails.OrderBy(p => p.IDProduct).Where(p => p.IDProduct != hangnhat && p.IDProduct != hangnhi).GroupBy(p => p.IDProduct).OrderByDescending(gr => gr.Count()).Select(g => g.Key).First()); //ban chay 3
                ViewBag.st1 = hangnhat;
                ViewBag.th2 = hangnhi;
                ViewBag.rd3 = hangba;
                //var hotnhat = db.OrderDetails.Where(s => s.Order.IDorder.Equals(s.IDOrder) && s.IDProduct.Equals(hangnhat)).ToList();
                var HotProduct = db.Products.Include(s => s.OrderDetails);
                return PartialView(HotProduct);
            }
            catch
            {
                var HotProduct = db.Products.Include(s => s.OrderDetails);
                return PartialView(HotProduct);
            }
            
        }
    }
}