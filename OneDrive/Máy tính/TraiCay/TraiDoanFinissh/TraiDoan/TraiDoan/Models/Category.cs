using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class Category
    {
        public Category()
        {
            this.Product = new HashSet<Products>();
        }
        [Key]
            public int IDCategory { get; set; }
            [Required]
            public string NameCategory { get; set; }
        public ICollection<Products> Product { get; set; }


        [NotMapped]
        public List<Category> catecolection { get; set; }
    }
}