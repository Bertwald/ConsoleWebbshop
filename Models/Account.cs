using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;

namespace TestWebbshopCodeFirst.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(35)]
        public string Username { get; set; } = null!;
        [StringLength(35)]
        public string Password { get; set; } = null!;
        public Privilege Privilege { get; set; }

        public virtual Person Person { get; set; } = null!;

        public override string ToString()
        {
            return "Username: " + Username + " Privilege: " + Privilege;
        }
    }
}
