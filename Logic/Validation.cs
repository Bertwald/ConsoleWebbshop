using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic
{
    internal class Validation
    {
        internal static UserData? ValidateUser(string userName, string passWord)
        {
            using (var db = new OurDbContext())
            {
                if (db.Accounts.Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).Any()) {
                    Account account = db.Accounts.Include("User").Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).First();
                    Person person = account.User;
                    Customer? customer = db.Customers.Where(x => x.Person.Equals(person)).FirstOrDefault();
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
