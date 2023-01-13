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
            if(LoggedInUser.Privilege == Privilege.Visitor) {
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
            }
        }
        public void PrintMenu()
        {
            string title = "Selected product menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            while (true)
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
                        bool ret = ShowInfo();
                        if (ret)
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
                        List<string> strings= new();
                        strings.AddRange(LoggedInUser.ProductsAsStrings());
                        strings.Add(LoggedInUser.GetSummary());
                        GUI.ShowShoppingCartItems(strings);

                        break;
                }
            }
        }

   

  

        private void Search() {
            Console.Write("Search: ");
            var search = InputModule.GetString();
            var result = ItemSelector<Product>.GetMatchingProductsInCategory(search, chosenCategory);
            GUI.PrintSelectedProducts(result, "Your search result for " + search);
            GUI.Delay();
        }

        private bool ShowInfo() {
            Product chosen = ItemSelector<Product>.GetItemFromList(products);
            return new DetailedProductPage(LoggedInUser, chosen).Run();

        }

        private void AddToShoppingCart() {
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
