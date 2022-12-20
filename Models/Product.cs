using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    internal class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public int? SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public double Vat { get; set; }
        public int? UnitsInStock { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
