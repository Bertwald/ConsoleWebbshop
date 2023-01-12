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

        internal List<Product> ProductsInShoppingCart()
        {
            return ShoppingCart.Products;          
        }
        internal List<string> ProductsAsStrings()
        {
            List<string> listOfStrings = new List<string>();
            foreach (var product in ProductsInShoppingCart())
            {
                listOfStrings.Add(product.ToString());
            }
            return listOfStrings;
        }
        internal string GetSummary()
        {
            decimal totalPrice = 0;
            foreach (var product in ProductsInShoppingCart())
            {
                totalPrice += product.Price;
            }
            int nrOfItems = ProductsInShoppingCart().Count;

            return $"You have {nrOfItems} Items in your cart for a total price of {totalPrice}.";
        }
    }
}
