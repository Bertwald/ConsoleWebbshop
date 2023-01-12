using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class ProductPage : IPage
    {
        private Category chosenCategory;
        private string headerText;
        private List<string> menu = new()
            {
                "Add to shopping cart",
                "Show more info",
                "Search within category",
                "Show shopping cart",
                "Back"
            };
        public UserData LoggedInUser { get; set; }
        public ProductPage(UserData user, Category chosenCategory)
        {
            LoggedInUser = user;
            chosenCategory = chosenCategory;
            headerText = $"";
        }
        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
            using (var db = new OurDbContext())
            {
                List<Product> products = db.Products.Where(x => x.Categories.Contains(chosenCategory)).ToList();
                Product? product = ItemSelector<Product>.GetItemFromList(products);
                GUI.PrintSelectedProducts(products, "Our fantastic clothes: ");
            }
        }
        public void PrintMenu()
        {
            string title = "Selected product menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            PrintHeader();
            PrintMenu();
            PrintFooter();
            int choice = InputModule.SelectFromList(menu);

            switch (choice)
            {
                case 1: //add to shopping cart
                    break;
                case 2: //show info
                    break;
                case 3: //search
                    Console.Write("Search: ");
                    var search = InputModule.GetString();
                    var result = ItemSelector<Product>.GetMatchingProductsInCategory(search, chosenCategory);
                    GUI.PrintSelectedProducts(result, "Your search result for " + search);
                    break;
                case 4: //back one step
                    break;
            }
            Console.ReadKey();
            return true;
        }

        public void PrintFooter()
        {

        }
    }
}
