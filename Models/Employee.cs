using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    internal class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public string? HireDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
