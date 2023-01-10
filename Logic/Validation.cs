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
        internal static Models.Account? ValidateUser(string userName, string passWord)
        {
            using (var db = new OurDbContext())
            {
                var loggedInUser = db.Accounts.Where(u => u.Username.Equals(userName) && u.Password.Equals(passWord)).Single();             

                return loggedInUser;
            }
        }
    }
}
