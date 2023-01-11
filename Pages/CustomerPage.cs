using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;
using TestWebbshopCodeFirst.Models;

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
        public Account LoggedInUser { get; set; }

        public CustomerPage(Account user)
        {
            LoggedInUser = user;
            headerText = "Welcome " + user.Username + " " + user.Privilege;
            if (user.Privilege == Logic.Privilege.Visitor)
            {
                menu = menu.Take(3).ToList();
            }
            using (var db = new OurDbContext())
            {
                selectedProducts = db.Products.Where(p => p.Categories.Select(x => x.Id ==10).SingleOrDefault()).Take(3).ToList();
            }
        }
        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
            GUI.PrintSelectedProducts(selectedProducts);
        }
        public void PrintMenu()
        {
            string title = "Startpage menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
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
