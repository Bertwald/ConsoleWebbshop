using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data {
    internal class AccountGenerator {
        public static List<Account> GenerateAccountsFor(List<Person> users, Privilege privilege) {
            List<Account> accounts = new();
            foreach (Person user in users) {
                Account account = new Account() {
                    Username = user.FirstName.PadRight(3, user.FirstName.LastOrDefault('x'))[0..3] +
                                                                     user.LastName.PadRight(6, user.LastName.LastOrDefault('y'))[3..6] +
                                                                     (user.BirthDate != null ? user.BirthDate.Replace("-", string.Empty)![^6..].ToString() : "1234"),
                    Password = string.Join(string.Empty, Enumerable.Range(0, 12)
                                                                     .Select(x => (x % 2 == 0) ?
                                                                        x / 2 < user.FirstName.Length ?
                                                                            user.FirstName[x / 2] :
                                                                            (char)('d' + x) :
                                                                        x / 2 < user.LastName.Length ?
                                                                            user.LastName[x / 2] :
                                                                            (char)('f' + x))),
                    Privilege = privilege,
                    User = user
                };
                user.Accounts.Add(account);
                accounts.Add(account);
            }
            return accounts;
        }
    }
}
