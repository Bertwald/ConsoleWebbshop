using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;

namespace TestWebbshopCodeFirst.Models
{
    public class Person : IPrintable
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


        public void Print()
        {
            Console.WriteLine(GetPersonalInfo());
        }
        public void PrintWithFields()
        {     
                Console.WriteLine($"BirtDate: {BirthDate} {Environment.NewLine} " +
                    $"FirstName: {FirstName} { Environment.NewLine} " +
                    $"LastName: { LastName} { Environment.NewLine}" +
                    $"Address: {Address} { Environment.NewLine}" +
                    $"PostalCode:  { PostalCode} {Environment.NewLine }" +
                    $"City: {City } {Environment.NewLine} " +
                    $"Country: {Country} {Environment.NewLine} " +
                    $"Telephone: {Telephone}  {Environment.NewLine} " +
                    $"MailAdress: {MailAdress} {Environment.NewLine}");            
        }
        public string GetPersonalInfo()
        {
            string personalInfo = BirthDate + Environment.NewLine +
                FirstName + " " + LastName + Environment.NewLine +
                Address + ", " + PostalCode + ", " + City + ", " + Country + Environment.NewLine +
                Telephone + Environment.NewLine +
                MailAdress + Environment.NewLine;

            return personalInfo;
        }
        public override string ToString()
        {

            string personalInfo = GetPersonalInfo();

            if (Customers.Any())
            {
                personalInfo += "Customer information: " + Environment.NewLine;
                foreach (Customer customer in Customers)
                {
                    personalInfo += "CreditCard number: " + customer.CreditCardNumber + Environment.NewLine;                    
                }
            }
            if (Employees.Any())
            {
                personalInfo += "Employee information: " + Environment.NewLine;
                foreach (Employee employee in Employees)
                {
                    personalInfo += "HireDate: " + employee.HireDate + Environment.NewLine;
                }
            }
            if (Accounts.Any())
            {
                personalInfo += "Account information: " + Environment.NewLine;
                foreach (Account account in Accounts)
                {
                    personalInfo += "Username: " + account.Username + " with ";
                    personalInfo += "Privilege: " + account.Privilege + Environment.NewLine;
                }
            }

            return personalInfo;
        }
    }
}
