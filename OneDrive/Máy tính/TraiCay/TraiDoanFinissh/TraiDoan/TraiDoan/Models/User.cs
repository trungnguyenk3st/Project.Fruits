using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class User
    {
        public User()
        {
            this.Orders = new HashSet<Orders>();
        }

        [Key]
        public int IDUser { get; set; }

        [Required(ErrorMessage = "Please enter FirtName!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName!")]
        public string LastName { get; set; }

        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [Required(ErrorMessage = "Please enter Email!")]
        public string Email { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [Required(ErrorMessage = "Please enter Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The password is not the same,Please enter again!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string LoginErrorMessage { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
