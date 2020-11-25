using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraiDoan.Models;

namespace TraiDoan.Controllers
{
    public class ShoppingCartController : Controller
    {
        DatabaseContext _db = new DatabaseContext();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //phương thúc add item vào giỏ hàng
        public ActionResult AddtoCart(int id)
        {
            string fragment = "#" + id.ToString();
            var pro = _db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return Redirect(Url.Action("Index", "Home") + fragment);
        }

        public ActionResult AddtoCart_shop(int id)
        {
            string fragment = "#" + id.ToString();
            var pro = _db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return Redirect(Url.Action("Products", "Home") + fragment);
        }
        public ActionResult AddtoCart_product(int id, int quantity)
        {

            var pro1 = _db.Products.SingleOrDefault(s => s.IDProduct == id);
            if (pro1 != null)
            {
                GetCart().Add_quan(pro1, quantity);
            }
            return Redirect(Url.Action("Details", "Home", new {id = id }));
        }
        //trang giỏ hàng
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("CartEmpty", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult CartEmpty()
        {
            return View();
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Sopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }


        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();

            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        public PartialViewResult totalview()
        {
            double _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Monney1();

            }
            ViewBag.infoCart = _t_item;
            return PartialView("totalview");
        }
        public ActionResult Shopping_sucssec()
        {
            return View();
        }

        public ActionResult CheckOut(FormCollection form)
        {
                try
                {
                    Cart cart = Session["Cart"] as Cart;
                    Orders _order = new Orders();
                    _order.OrderDate = DateTime.Now;
                    _order.Namecus = form["Namecus"];
                    _order.Phonecus = form["Phonecus"];
                    _order.Adresscus = form["Adresscus"];
                    _order.AdressDelivery = form["AddressDeli"];
                    _db.Orders.Add(_order);


                    foreach (var item in cart.Items)
                    {
                        OrderDetails _order_Detail = new OrderDetails();
                        _order_Detail.IDOrder = _order.IDorder;
                        _order_Detail.IDProduct = item._shopping_product.IDProduct;
                        _order_Detail.UnitPriceSale = item._shopping_product.UnitPrice;
                        _order_Detail.QuantitySale = item._shopping_quantity;
                        _db.OrderDetails.Add(_order_Detail);
                    }
                    _db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("Shopping_sucssec", "ShoppingCart");
                }
                catch
                {
                    return Content("Erro");
                }              
        }
        public ActionResult CheckContact(FormCollection form)
        {
            try
            {
                Contact _contact = new Contact();
                _contact.Name = form["Cname"];
                _contact.Email = form["Cemail"];
                _contact.Datacontact = form["Cdata"];
                _db.Contact.Add(_contact);
                _db.SaveChanges();
                return RedirectToAction("Contact_sucsses", "ShoppingCart");
            }                      
            catch
            {
                return Content("Erro");
            }
        }
        public ActionResult Contact_sucsses()
        {
            return View();
        }
    }
}