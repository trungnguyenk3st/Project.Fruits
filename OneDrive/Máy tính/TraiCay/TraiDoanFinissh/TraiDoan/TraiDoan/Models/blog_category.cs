using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class blog_category
    {
        [Key]
        public int IDblogcate { get; set; }
        [Required]
        public string Nameblogcate { get; set; }

        public ICollection<blog> blog { get; set; }
    }
}