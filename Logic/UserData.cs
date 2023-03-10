using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        internal string GetSummary(bool vatIncluded = false, bool shippingIncluded = false) {
            if (ShoppingCart.OrderDetails.Any()) {
                double totalPrice = 0;
                int nrOfItems = 0;
                for (int index = 0; index < ProductsInShoppingCart().Count; index++) {
                    if (ShoppingCart.OrderDetails.Any()) {
                        totalPrice += ProductsInShoppingCart()[index].Price * (vatIncluded ? (1d + ProductsInShoppingCart()[index].Vat / 100) : 1d) * ShoppingCart.OrderDetails.ToList()[index].Quantity;
                        nrOfItems += ShoppingCart.OrderDetails.ToList()[index].Quantity;
                    } else {
                        totalPrice += ProductsInShoppingCart()[index].Price * (vatIncluded ? (1d + ProductsInShoppingCart()[index].Vat / 100) : 1d);
                        nrOfItems++;
                    }
                }
                totalPrice = Math.Round(totalPrice, 2);
                return $"You have {nrOfItems} Items in your cart for a total price of " +
                    $"{(shippingIncluded ? totalPrice + ItemSelector<Product>.GetShippingCost(ShoppingCart.ShippingOption) : totalPrice)}" +
                    $"{(vatIncluded ? (shippingIncluded ? " § Inc VAT and Shipping." : " § Inc VAT.") : " §.")}";
            } else {
                int nrOfItems = ProductsInShoppingCart().Count;
                return $"You have {nrOfItems} Items in your cart";
            }
        }

        internal double GetTotalPrice() {
            double totalPrice = 0;
            if (ShoppingCart.OrderDetails.Any()) {
                for (int index = 0; index < ProductsInShoppingCart().Count; index++) {
                    totalPrice += ProductsInShoppingCart()[index].Price *
                                 (1d + ProductsInShoppingCart()[index].Vat / 100) *
                                  ShoppingCart.OrderDetails.ToList()[index].Quantity;
                }
            }
            return totalPrice;
        }
        internal Customer GetCustomer()
        {
            return Person.Customers.FirstOrDefault(new Customer());           
        }


    }
}
