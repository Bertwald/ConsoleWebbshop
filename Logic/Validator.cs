using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic
{
    internal class Validator
    {
        internal static UserData? ValidateUser(string userName, string passWord)
        {
            using (var db = new WebshopDbContext())
            {
                if (db.Accounts.Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).Any()) {
                    Account account = db.Accounts.Include(x => x.Person).ThenInclude(x => x.Customers).Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).First();
                    Person person = account.Person;
                    Customer? customer = person.Customers.FirstOrDefault();  //db.Customers.Where(x => x.Person.Equals(person)).FirstOrDefault();
                    if(customer == null) {
                        customer = new Customer();
                    }
                    return new UserData(account, person, customer);
                } else {
                    return null;
                }
                //var loggedInUser = db.Accounts.Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).Single();             
            }
        }
    }
}
