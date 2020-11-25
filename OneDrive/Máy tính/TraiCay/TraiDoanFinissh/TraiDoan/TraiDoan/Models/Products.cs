using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class Products
    {
        public Products()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            Image = "~/Content/img/logo.png";
        }

        //public class CartItem1
        //{
        //    public Products _shopping_product { get; set; }
        //    public int _shopping_quantity { get; set; }
        //}

        //List<CartItem1> items = new List<CartItem1>();
        //public IEnumerable<CartItem1> Items
        //{
        //    get { return items; }
        //}
        //public void Add(Products _pro, int _quantity)
        //{
        //    var item = items.FirstOrDefault(s => s._shopping_product.IDProduct == _pro.IDProduct);
        //    if (item == null)
        //    {
        //        items.Add(new CartItem1
        //        {
        //            _shopping_product = _pro,
        //            _shopping_quantity = _quantity
        //        });
        //    }
        //    else { item._shopping_quantity += _quantity; }

        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDProduct { get; set; }
        [Required]
        public string NameProduct { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public string Image { get; set; }
        public int IDCategory { get; set; }
        [ForeignKey("IDCategory")]
        public Category Category { get; set; }


       


        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

    }
}