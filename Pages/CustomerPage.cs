using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;
using TestWebbshopCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using TestWebbshopCodeFirst.Logic;

namespace TestWebbshopCodeFirst.Pages {
    internal class CustomerPage : IPage {
        private List<string> menu = new()
            {
                "Browse Inventory by category",
                "Search for products",
                "PlaceHolder",
                "Account information",
                "Show shopping cart"
            };
        private List<Product> selectedProducts = new();

        private string headerText;
        private readonly int numberOfSelectedToDisplay = 3;

        public UserData LoggedInUser { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerPage(UserData user) {
            LoggedInUser = user;
            SetHeaderText();
            if (user.Privilege == Logic.Privilege.Visitor) {
                menu[2] = "To Login Page";
                menu = menu.Take(3).ToList();
            } else {
                menu[2] = "Logout";
            }
            RetrieveSelectedItems();
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private void SetHeaderText() {
            headerText = LoggedInUser.Privilege == Logic.Privilege.Visitor ?
                "Welcome Honored Guest" + Environment.NewLine +
                "Feel free to browse out exquisite inventory!" + Environment.NewLine + "When you decide to buy one or many of our excellent " +
                "products" + Environment.NewLine + "You will need a registered account to purchase said items from our collection of fine merchandise" :
                "Welcome back dear " + LoggedInUser.Privilege + " " + LoggedInUser.Username + Environment.NewLine +
                "We eagerly await your next order";
        }

        private void RetrieveSelectedItems() {
            using (var db = new OurDbContext()) {
                selectedProducts = db.Products
                                     .Where(p => p.Categories
                                                  .Where(category => category.Id == 10)
                                                  .Count() > 0)
                                     .Take(numberOfSelectedToDisplay)
                                     .ToList();
            }
        }

        public void PrintHeader() {
            GUI.PrintHeader(new List<string> { headerText });
            GUI.PrintSelectedProducts(selectedProducts);
        }
        public void PrintMenu() {
            string title = "Customer Shopping menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run() {
            GUI.SetWindowTitle(this, LoggedInUser.Privilege);
            bool exit = false;
            while (!exit) {
                GUI.ClearWindow();
                PrintHeader();
                PrintMenu();
                PrintFooter();
                int choice = InputModule.SelectFromList(menu);
                GUI.ClearWindow();
                switch (choice) {
                    case 1: //choose category
                        exit = ChooseCategory();
                        break;
                    case 2: //search                           
                        Search();
                        break;
                    case 3: //login/logout
                        return false;
                    case 4: //account info
                        DisplayAccountInformation();
                        break;
                    case 5: //shoppingcart
                        exit = new ShoppingCartPage(LoggedInUser).Run();

                        //List<string> strings = new();
                        //strings.AddRange(LoggedInUser.ProductsAsStrings());
                        //strings.Add(LoggedInUser.GetSummary());
                        //GUI.ShowShoppingCartItems(strings);
                        break;
                }
            }
            return false;
        }

        private void DisplayShoppingCart() {
            var shoppingCart = LoggedInUser.ProductsAsStrings();
            foreach (var product in shoppingCart) {
                Console.WriteLine(product);
            }
            string cartInfo = LoggedInUser.GetSummary();
            Console.WriteLine(cartInfo);
            GUI.Delay();
        }

        private void DisplayAccountInformation() {
            var miniMenu = new List<string> { "Personal details", "Order details" };
            GUI.PrintMenu("Your account", miniMenu);
            int menuChoice = InputModule.SelectFromList(miniMenu);

            switch (menuChoice) {
                case 1:
                    List<Person> personalInformation;
                    using (var db = new OurDbContext()) {
                        personalInformation = db.Persons
                                        .Where(p => p.Id == LoggedInUser.Person.Id)
                                        .Include(p => p.Accounts)
                                        .Include(p => p.Employees)
                                        .Include(p => p.Customers).ToList();
                    }
                    GUI.PrintSelectedProducts<Person>(personalInformation);
                    GUI.Delay();
                    break;
                case 2:
                    //Orderdetails
                    break;

            }
        }

        private bool ChooseCategory() {
            List<Category> categories;
            using (var db = new OurDbContext()) {
                categories = db.Categories.ToList();
            }
            Category? category = ItemSelector<Category>.GetItemFromList(categories);
            return new BrowseCategoryPage(LoggedInUser, category).Run();
        }

        private static void Search() {
            Console.Write("Search: ");
            var search = InputModule.GetString();
            var result = ItemSelector<Product>.GetMatchingProducts(search);
            GUI.PrintSelectedProducts(result, "Here is your search result for " + search);
            GUI.Delay();
        }

        public void PrintFooter() {
        }


    }
}
