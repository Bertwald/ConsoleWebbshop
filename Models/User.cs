using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    internal class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            UserAccounts = new HashSet<UserAccount>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? BirthDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int PostalCode { get; set; }
        public string Country { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string MailAdress { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
