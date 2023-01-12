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
        private List<Product> products;
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
            this.chosenCategory = chosenCategory;
            headerText = $"";
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
                    case 1: //add to shopping cart
                        Product chosen = ItemSelector<Product>.GetItemFromList(products);
                        LoggedInUser.ShoppingCart.Products.Add(chosen);
                        Console.WriteLine($"1 {chosen} has been added to your shopping cart");
                        Console.ReadKey(true);
                        //return false;
                        break;
                    case 2:
                        chosen = ItemSelector<Product>.GetItemFromList(products);//show info
                        bool ret = new DetailedProductPage(LoggedInUser, chosen).Run();
                        if (ret)
                        {
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    case 3: //search
                        Console.Write("Search: ");
                        var search = InputModule.GetString();
                        var result = ItemSelector<Product>.GetMatchingProductsInCategory(search, chosenCategory);
                        GUI.PrintSelectedProducts(result, "Your search result for " + search);
                        break;
                    case 4: //back one step
                        var shoppingCart = LoggedInUser.ProductsAsStrings();
                        foreach (var product in shoppingCart)
                        {
                            Console.WriteLine(product);
                        }
                        string cartInfo = LoggedInUser.GetSummary();
                        Console.WriteLine(cartInfo);
                        Console.ReadKey(true);
                        break;
                    case 5:
                        return true;
                }
            }
            //Console.ReadKey();
            //return true;
        }

        public void PrintFooter()
        {

        }
    }
}
