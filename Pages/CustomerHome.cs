using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;
using TestWebbshopCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Interfaces;

namespace TestWebbshopCodeFirst.Pages
{
    internal class CustomerHome : IPage {
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
        public CustomerHome(UserData user) {
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
                "Welcome back dear " + LoggedInUser.Privilege + " " + LoggedInUser.Person.FirstName + " " + LoggedInUser.Person.LastName + Environment.NewLine +
                "We eagerly await your next order";
        }

        private void RetrieveSelectedItems() {
            using (var db = new WebshopDbContext()) {
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
                        return true;
                    case 4: //account info
                        DisplayAccountInformation();
                        break;
                    case 5: //shoppingcart
                        exit = new ShoppingCart(LoggedInUser).Run();
                        break;
                }
            }
            return true;
        }

        private void DisplayShoppingCart() {
            var shoppingCart = LoggedInUser.ProductsAsStrings();
            foreach (var product in shoppingCart) {
                Console.WriteLine(product);
            }
            string cartInfo = LoggedInUser.GetSummary();
            Console.WriteLine(cartInfo);
        }

        private void DisplayAccountInformation() {
            var miniMenu = new List<string> { "Personal details", "Order details" };
            GUI.PrintMenu("Your account", miniMenu);
            int menuChoice = InputModule.SelectFromList(miniMenu);

            switch (menuChoice) {
                case 1:
                    List<Person> personalInformation;
                    using (var db = new WebshopDbContext()) {
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
                    List<Order> orders;
                    using (var db = new WebshopDbContext())
                    {
                        orders = db.Orders.Where(x => x.Custumer == LoggedInUser.GetCustomer()).Include(y => y.OrderDetails).ToList();
                    }
                    GUI.PrintMenu("Your orders", orders.Select(x => x.ToString()).ToList());
                    GUI.Delay();
                    break;
            }
        }

        private bool ChooseCategory() {
            List<Category> categories;
            using (var db = new WebshopDbContext()) {
                categories = db.Categories.Where(x => !x.CategoryName.Equals("Selected")).ToList();
            }
            Category? category = ItemSelector<Category>.GetItemFromList(categories);
            return new CategoryList(LoggedInUser, category).Run();
        }

        private void Search() {
            Console.Write("Search: ");
            var search = InputModule.GetString();
            var result = ItemSelector<Product>.GetMatchingProducts(search);          
            if (result.Any())
            {
                GUI.PrintSelectedProducts(result, "Your search result for " + search);
                GUI.PrintMenu("Search Results", result.Select(x => x.ToString()).ToList());
                var selectedProduct = ItemSelector<Product>.GetItemFromList(result);
                new ProductDetail(LoggedInUser, selectedProduct).Run();
            }
            else
            {
                Console.WriteLine("Unfortunately we couldn't find any " + search);
                GUI.Delay();
            }
        }

        public void PrintFooter() {
        }


    }
}
