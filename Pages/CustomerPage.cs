using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;
using TestWebbshopCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace TestWebbshopCodeFirst.Pages
{
    internal class CustomerPage : IPage
    {
        private List<string> menu = new()
            {
                "Choose category",
                "Search for products",
                "Log in / log out",
                "Account information",
                "Show shopping cart"
            };
        private List<Product> selectedProducts = new();

        private string headerText;
        private readonly int numberOfSelectedToDisplay = 3;

        public Account LoggedInUser { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerPage(Account user) {
            LoggedInUser = user;
            SetHeaderText();
            if (user.Privilege == Logic.Privilege.Visitor) {
                menu = menu.Take(3).ToList();
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

        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
            GUI.PrintSelectedProducts(selectedProducts);
        }
        public void PrintMenu()
        {
            string title = "Customer Shopping menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            GUI.ClearWindow();
            GUI.SetWindowTitle(this, LoggedInUser.Privilege);
            PrintHeader();
            PrintMenu();
            PrintFooter();
            int choice = InputModule.SelectFromList(menu);

            Console.ReadKey();
            return true;
        }

        public void PrintFooter()
        {
        }


    }
}
