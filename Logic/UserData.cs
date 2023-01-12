using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic {
    internal class UserData {
        public UserData(Account account, Person person, Customer customer) {
            Username = account.Username;
            Privilege = account.Privilege;
            Person = person;
            ShoppingCart = new ();
            ShoppingCart.Orderstatus = OrderStatus.InShoppingCart;
            ShoppingCart.Custumer = customer;
        }
        internal string Username { get; set; }
        internal Privilege Privilege { get; set; }
        internal Person Person { get; set; }
        internal Order ShoppingCart { get; set; }
    }
}
