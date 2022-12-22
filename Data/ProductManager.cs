using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data
{
    public class ProductManager
    {
        public static List<Product> Products { get; set; }

        public static List<Product> GetAllProducts()
        {
            if (Products == null || !Products.Any())
            {
                Products = new List<Product>() {
                    new Product() { Name = "T-Shirt", Price = 99, Vat = 25, UnitsInStock = 9, Description = "Cool red T-Shirt with a knight.", LongDescription = "Cool red T-Shirt with knights walking to find the Holy Grail." },
                    new Product() { Name = "T-Shirt", Price = 99, Vat = 25, UnitsInStock = 12, Description = "Cool green plain T-Shirt.", LongDescription = "Cool green plain cotton T-Shirt for multiple occasions." },
                    new Product() { Name = "Shorts", Price = 149, Vat = 25, UnitsInStock = 14, Description = "Cotton shorts with a print of a bridge.", LongDescription = "Soft cotton shorts with a bridetheme and its old guardian." },
                    new Product() { Name = "Shorts", Price = 149, Vat = 25, UnitsInStock = 15, Description = "Jeansshorts", LongDescription = "Blue jeansshorts with a mission to make you look good!" },
                    new Product() { Name = "Pants", Price = 299, Vat = 25, UnitsInStock = 18, Description = "Classic blue jeans.", LongDescription = "Classic blue jeans with alot of swagger." },
                    new Product() { Name = "Pants", Price = 299, Vat = 25, UnitsInStock = 17, Description = "Soft linen pants", LongDescription = "Soft linen pants for everyday use." },
                    new Product() { Name = "Cap", Price = 199, Vat = 25, UnitsInStock = 11, Description = "Black cap with no print.", LongDescription = "Classic black cap. You can even wear it backwards!" },
                    new Product() { Name = "Cap", Price = 199, Vat = 25, UnitsInStock = 21, Description = "White cap.", LongDescription = "White cap with a killerrabbit." },                   
                };
            }

            return Products;
        }
    }
}
