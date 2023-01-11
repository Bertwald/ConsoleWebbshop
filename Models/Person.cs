using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Models
{
    public class Person
    {
        public Person()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Accounts = new HashSet<Account>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(13)]
        public string? BirthDate { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; } = null!;
        [StringLength(255)]
        public string LastName { get; set; } = null!;
        [StringLength(255)]
        public string Address { get; set; } = null!;
        [StringLength(255)]
        public string City { get; set; } = null!;
        public int PostalCode { get; set; }
        [StringLength(255)]
        public string Country { get; set; } = null!;
        [StringLength(255)]
        public string Telephone { get; set; } = null!;
        [StringLength(255)]
        public string MailAdress { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public override string ToString()
        {
            return BirthDate + Environment.NewLine + 
                FirstName + " " + LastName + Environment.NewLine + 
                Address + " " + PostalCode + " " + City + " " + Country + Environment.NewLine + 
                Telephone + Environment.NewLine +
                MailAdress;
        }
    }
}
