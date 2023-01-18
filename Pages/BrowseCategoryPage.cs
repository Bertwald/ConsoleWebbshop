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
    internal class BrowseCategoryPage : IPage
    {
        private List<Product> products;
        private Category chosenCategory;
        private string headerText;
        private List<string> menu = new()
            {
                "Show more info",
                "Search within category",
                "Back",
                "Add to shopping cart",
                "Show shopping cart",
            };
        public UserData LoggedInUser { get; set; }
        public BrowseCategoryPage(UserData user, Category chosenCategory)
        {
            LoggedInUser = user;
            this.chosenCategory = chosenCategory;
            headerText = $"{this.chosenCategory.CategoryName} : {this.chosenCategory.Description}";
            if (LoggedInUser.Privilege == Privilege.Visitor)
            {
                menu = menu.Take(3).ToList();
            }
        }
        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
            using (var db = new OurDbContext())
            {
                products = db.Products.Where(x => x.Categories.Contains(chosenCategory)).ToList();
                GUI.PrintSelectedProducts(products, $"Our fantastic {chosenCategory}: ");
                GUI.PrintMenu(chosenCategory.CategoryName, products.Select(x => x.ToString()).ToList());
            }
        }
        public void PrintMenu()
        {
            string title = "Selected product menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            bool exit = false;

            while (!exit)
            {
                GUI.ClearWindow();
                PrintHeader();
                PrintMenu();
                PrintFooter();
                int choice = InputModule.SelectFromList(menu);
                GUI.ClearWindow();
                switch (choice)
                {
                    case 1: // Show detailed information
                        bool back = ShowInfo();
                        if (back)
                        {
                            return false;
                        }
                        break;
                    case 2: // Search within category
                        Search();
                        break;
                    case 3: // Back one step
                        return false;
                    case 4: // Add To cart
                        AddToShoppingCart();
                        break;
                    case 5: // Show Cart
                        exit = new ShoppingCartPage(LoggedInUser).Run();

                        //List<string> strings= new();
                        //strings.AddRange(LoggedInUser.ProductsAsStrings());
                        //strings.Add(LoggedInUser.GetSummary());
                        //GUI.ShowShoppingCartItems(strings);

                        break;
                }
            }
            return true;
        }

        private void Search()
        {
            Console.Write("Search: ");
            var search = InputModule.GetString();
            var result = ItemSelector<Product>.GetMatchingProductsInCategory(search, chosenCategory);
            if (result.Any())
            {
                GUI.PrintSelectedProducts(result, "Your search result for " + search);
                GUI.PrintMenu(chosenCategory.CategoryName, products.Select(x => x.ToString()).ToList());              
                var selectedProduct = ItemSelector<Product>.GetItemFromList(result);
                new DetailedProductPage(LoggedInUser, selectedProduct).Run();
            }
            else
            {
                Console.WriteLine("Unfortunately we couldn't find any " + search);
                GUI.Delay();
            }
        }

        private bool ShowInfo()
        {
            Product chosen = ItemSelector<Product>.GetItemFromList(products);
            return new DetailedProductPage(LoggedInUser, chosen).Run();

        }

        private void AddToShoppingCart()
        {
            Product chosen = ItemSelector<Product>.GetItemFromList(products);
            LoggedInUser.ShoppingCart.Products.Add(chosen);
            Console.WriteLine($"1 {chosen} has been added to your shopping cart");
            GUI.Delay();
        }

        public void PrintFooter()
        {

        }
    }
}
