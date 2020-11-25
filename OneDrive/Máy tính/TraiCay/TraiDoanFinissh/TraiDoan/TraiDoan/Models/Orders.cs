using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class Orders
    {
        public Orders()
        {
            this.OrderDetails = new HashSet<OrderDetails>();

        }
        [Key]
        public int IDorder { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required]

        public string Namecus { get; set; }
        public string Adresscus { get; set; }
        public string Phonecus { get; set; }
        public string AdressDelivery { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}