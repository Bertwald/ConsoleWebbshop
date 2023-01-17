using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Logic
{
    internal class ItemSelector<T> where T : class
    {
        internal static T? GetItemFromList(List<T> items)
        {
            int itemIndex = 0;
            if (!items.Any())
            {
                return null;
            }

            List<string> itemStrings = new List<string>();
            foreach (T item in items)
            {
                itemStrings.Add(item.ToString()!);
            }
            GUI.PrintMenu("Please select from " + typeof(T).Name + " menu", itemStrings);
            if (items.Count < 10)
            {
                itemIndex = InputModule.SelectFromList(itemStrings);
            }
            else
            {
                itemIndex = InputModule.GetBigIntInRange(1, items.Count + 1);
            }
            return items[itemIndex - 1];
        }

        internal static List<Product> GetMatchingProducts(string search)
        {
            List<Product> items = new List<Product>();
            using (var db = new OurDbContext())
            {

                List<Product> products = db.Products
                                        .Where(x => x.Name
                                            .Contains(search) || x.Description
                                            .Contains(search) || x.LongDescription
                                            .Contains(search))
                                        .ToList();
                items.AddRange(products);


            }
            return items;
        }

        internal static List<Product> GetMatchingProductsInCategory(string search, Category category)
        {
            List<Product> items = new List<Product>();
            using (var db = new OurDbContext())
            {

                List<Product> products = db.Products
                                        .Where(x => x.Categories.Contains(category) && (x.Name
                                            .Contains(search) || x.Description
                                            .Contains(search) || x.LongDescription
                                            .Contains(search)))
                                        .ToList();
                items.AddRange(products);


            }
            return items;
        }

        internal static double GetShippingCost(ShippingOption option)
        {
            return (double)(option - 1) * 50d;
        }
    }
}
