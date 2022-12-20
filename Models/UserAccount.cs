using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    internal class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
        public string Privileges { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
