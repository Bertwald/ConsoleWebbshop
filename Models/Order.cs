using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Net;

namespace TestWebbshopCodeFirst.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Products = new List<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustumerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public PayingOption PayingOption { get; set; }
        public decimal? TotalPrice { get; set; }
        public ShippingOption ShippingOption { get; set; }
        [StringLength(255)]
        public string? ShippingAdress { get; set; }
        [StringLength(255)]
        public string? ShippingCity { get; set; }
        public int? ShippingPostalcode { get; set; }
        [StringLength(255)]
        public string? ShippingCountry { get; set; }
        public OrderStatus Orderstatus { get; set; }

        public virtual Customer Custumer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [NotMapped]
        internal List<Product> Products { get; set; }


        internal string GetAlternativeAddress()
        {
            string alternativeAddress = 
            ShippingAdress + ", " + ShippingPostalcode + ", " + ShippingCity + ", " + ShippingCountry + Environment.NewLine;
               
            return alternativeAddress;
        }
    }
}
