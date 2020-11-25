using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class blog
    {
        public blog()
        {
            img1 = "~/Content/img/logo.png";
            img2 = "~/Content/img/logo.png";
            img3 = "~/Content/img/logo.png";
        }

        [Key]
        public int IDblog { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string sortcontent { get; set; }
        public string content { get; set; }
        public string comment { get; set; }
        public string author { get; set; }
        public string like { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateblog { get; set; }  

        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }

        public int IDblogcate { get; set; }
        [ForeignKey("IDblogcate")]
        public blog_category blogcate { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload3 { get; set; }

    }
}