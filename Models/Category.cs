using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    public class Category
    {
        public Category() {
            Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(35)]
        public string CategoryName { get; set; } = null!;
        [StringLength(255)]
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public override string ToString() {
            return CategoryName;
        }
    }
}
