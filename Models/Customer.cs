using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }      

        [StringLength(20)]
        public string? CreditCardNumber { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return "User Id: " + UserId;
        }
    }
}
