using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TraiDoan.Models
{
    public class OrderDetails
    {
        [Key]
        public int IDOrderDetail { get; set; }

        public int QuantitySale { get; set; }
        public double UnitPriceSale { get; set; }
        public int IDOrder { get; set; }
        [ForeignKey("IDOrder")]
        public Orders Order { get; set; }
        public int IDProduct { get; set; }
        [ForeignKey("IDProduct")]
        public Products Products { get; set; }

    }
}