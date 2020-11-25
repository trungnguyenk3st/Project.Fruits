using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class Contact
    {
        [Key]
        public int IDcontact { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Datacontact { get; set; }
    }
}