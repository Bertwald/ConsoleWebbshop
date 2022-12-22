using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data
{
    internal class CustomerManager
    {
        public static List<Customer> Customers { get; set; }

        public static List<Customer> GetAllCustomers()
        {
            if (Customers == null || !Customers.Any())
            {
                Customers = new List<Customer>() {
                    new Customer() { UserId = 1, ShippingAdresses  = "Kungsgatan 15, 10432, Stockholm, Sverige", CreditCardNumber = "1234 5678 9874 5612"},
                    new Customer() { UserId = 2, ShippingAdresses  = "Spjutvägen 2, 34568, Kiruna, Sverige", CreditCardNumber = "9874 5678 9874 6541"  },
                    new Customer() { UserId = 3, ShippingAdresses  = "Östanvägen 14, 61170, Jönåker, Sverige", CreditCardNumber = "9635 8521 1257 2148"},
                    new Customer() { UserId = 4, ShippingAdresses  = "Gruvvgägen 15, 61165, Nyköping, Sverige", CreditCardNumber = "6985 2147 3268 1269"},
                };
            }

            return Customers;
        }
    }
}
