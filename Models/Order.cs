using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    internal class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int CustumerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PayingOption { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? ShippingOption { get; set; }
        public string? ShippingAdress { get; set; }
        public string? ShippingCity { get; set; }
        public int? ShippingPostalcode { get; set; }
        public string? ShippingCountry { get; set; }
        public string? Orderstatus { get; set; }

        public virtual Customer Custumer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
