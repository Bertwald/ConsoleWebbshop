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
        public static List<int> Categories { get; set; }

        public static List<Product> GetAllProducts()
        {
            if (Products == null || !Products.Any())
            {
                Products = new List<Product>() {
                    (new Product() { Name = "T-Shirt", Price = 99, Vat = 25, UnitsInStock = 9, Description = "Cool red T-Shirt with a knight.", LongDescription = "Cool red T-Shirt with knights walking to find the Holy Grail." }),
                    new Product() { Name = "T-Shirt", Price = 99, Vat = 25, UnitsInStock = 12, Description = "Cool green plain T-Shirt.", LongDescription = "Cool green plain cotton T-Shirt for multiple occasions." },
                    new Product() { Name = "Shorts", Price = 149, Vat = 25, UnitsInStock = 14, Description = "Cotton shorts with a print of a bridge.", LongDescription = "Soft cotton shorts with a bridetheme and its old guardian." },
                    new Product() { Name = "Shorts", Price = 149, Vat = 25, UnitsInStock = 15, Description = "Jeansshorts", LongDescription = "Blue jeansshorts with a mission to make you look good!" },
                    new Product() { Name = "Pants", Price = 299, Vat = 25, UnitsInStock = 18, Description = "Classic blue jeans.", LongDescription = "Classic blue jeans with alot of swagger." },
                    new Product() { Name = "Pants", Price = 299, Vat = 25, UnitsInStock = 17, Description = "Soft linen pants", LongDescription = "Soft linen pants for everyday use." },
                    new Product() { Name = "Cap", Price = 199, Vat = 25, UnitsInStock = 11, Description = "Black cap with no print.", LongDescription = "Classic black cap. You can even wear it backwards!" },
                    new Product() { Name = "Cap", Price = 199, Vat = 25, UnitsInStock = 21, Description = "White cap.", LongDescription = "White cap with a killerrabbit." },
                    new Product() { Name = "Coconut", Price = 2.40M, Vat = 6, UnitsInStock = 1, Description = "A Coconut from Africa", LongDescription = "A very rare and exclusive african coconut with many uses including but not limited to: Drink Ingredient, Drink Container, Instrument of Death, Sound Effects tool, Rudimentary football, Exercise tool and more! As Enjoyed by the famous English King from legends"},
                    new Product() { Name = "StraitJacket", Price = 942.40M, Vat = 12, UnitsInStock = 17, Description = "A very long sleeved shirt", LongDescription = "A very popular outfit at special parties, High quality cotton with leather straps. Very comfortable and with many settings and fixtures. CEOs favorite! One Size Restricts all!"},
                    new Product() { Name = "Hawaii Shirt", Price = 32.40M, Vat = 25, UnitsInStock = 144, Description = "Shirt with classic motif", LongDescription = "Classy gentleman shirt with high quality leafy pattern, non-iron, non-shrink, non-wash model with factory shrinkles and smell of quality Rhum"},
                    new Product() { Name = "Crocs", Price = 102.40M, Vat = 12, UnitsInStock = 56, Description = "Footwear for all occasions", LongDescription = "A classic unisex footwear suitable for all ages. Choking hazard"},
                    new Product() { Name = "Straw Hat", Price = 12.40M, Vat = 12, UnitsInStock = 19, Description = "Comfortable summer hat", LongDescription = "Meticously handcrafted from organic high quality straw from Järna. Gives the wearer an aura of professionalism. Also suitable for scarecrows"},
                };
            }

            return Products;
        }
        public static List<int> GetCategoryIndices() {
            if (Categories == null || !Categories.Any()) {
                Categories = new List<int>() {
                    0,
                    0,
                    1,
                    1,
                    1,
                    1,
                    5,
                    5,
                    8,
                    2,
                    7,
                    6,
                    5
                };
            }

            return Categories;
        }
    }
}
