using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;


        public override string ToString()
        {
            string orderDetails;
            using (var db = new OurDbContext())
            {
                var product = db.Products.Where(p => p.Id == ProductId).First();
                orderDetails = $"Product: {product.Name} Unitprice: {UnitPrice} Quantity: {Quantity}";
            }
            return orderDetails;
        }
    }


}
