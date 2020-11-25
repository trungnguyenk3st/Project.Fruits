using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiDoan.Models;

namespace TraiDoan.Controllers
{
    public class LoginController : CheckLoginController
    {
        DatabaseContext _db = new DatabaseContext();
        // GET: Login
        public ActionResult Index()
        {          
                return View();           
        }
        //method Login
        [HttpPost]
        public ActionResult Authen(User _user)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email)
             && s.Password.Equals(_user.Password)).FirstOrDefault();

            //Session["IDUser"] = _user.IDUser;
        
            //Session["Adress"] = _user.FirstName;
            //Session["Phonenumber"] = _user.LastName;
            if (check == null)
            {
                _user.LoginErrorMessage = "Error Email or Password! Try again please!";
                return View("Index", _user);

            }
            else // login eccessfull
            {              
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                Session["Email"] = _user.Email;
                if (test.Email !="admin")//khong phai admin
                {
                    Session.Add("IDUser", _user.IDUser);                   
                    return RedirectToAction("Index", "Home");
                }
                else // neu la admin
                {
                    Session.Add("Admin", _user.Email);
                    return RedirectToAction("Index", "Categories");
                }
               
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //method xử lý model
        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    // nếu mà đk thành công thì chuyển hướng trang register sang login
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email alredy exists! Use another email please!";
                    return View();
                }
            }
            return View();
        }

        //public ActionResult admin(User _user)
        //{
        //    try
        //    {
        //        var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
        //        if (test.Email != "admin")//khong phai admin
        //        {
        //            Content("Ban khong phai admin");
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else // neu la admin
        //        {
        //            Session.Add("Admin", _user.IDUser);
        //            return RedirectToAction("Index", "Categories");
        //        }
        //    }
        //    catch
        //    {
        //        Content("Vui long dang nhap addmin");
        //        return RedirectToAction("Index", "Login");
        //    }
        //}

    }
}