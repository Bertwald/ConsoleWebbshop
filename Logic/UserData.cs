using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Logic {
    internal class UserData {
        public UserData(Account account, Person person, Customer customer) {
            Username = account.Username;
            Privilege = account.Privilege;
            Person = person;
            ShoppingCart = new() {
                Orderstatus = OrderStatus.InShoppingCart,
                Custumer = customer
            };
        }
        internal string Username { get; set; }
        internal Privilege Privilege { get; set; }
        internal Person Person { get; set; }
        internal Order ShoppingCart { get; set; }

        internal List<Product> ProductsInShoppingCart() {
            return ShoppingCart.Products;
        }
        internal List<string> ProductsAsStrings() {
            List<string> listOfStrings = new List<string>();
            foreach (var product in ProductsInShoppingCart()) {
                listOfStrings.Add(product.ToString());
            }
            return listOfStrings;
        }
        internal string GetSummary() {
            if (ShoppingCart.OrderDetails.Any()) {
                decimal totalPrice = 0;
                for (int index = 0; index < ProductsInShoppingCart().Count; index++) {
                    if (ShoppingCart.OrderDetails.Any()) {
                        totalPrice += ProductsInShoppingCart()[index].Price * ShoppingCart.OrderDetails.ToList()[index].Quantity;
                    } else {
                        totalPrice += ProductsInShoppingCart()[index].Price;
                    }
                }
                int nrOfItems = ProductsInShoppingCart().Count;
                return $"You have {nrOfItems} Items in your cart for a total price of {totalPrice}.";
            } else {
                int nrOfItems = ProductsInShoppingCart().Count;
                return $"You have {nrOfItems} Items in your cart";
            }
        }


    }
}
