using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;

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
        public CustomerPage(Models.Account user)
        {
            LoggedInUser = user;
            headerText = "Welcome customer " + LoggedInUser.Username + " " + LoggedInUser.Privilege;
            if (user.Privilege == Logic.Privilege.Visitor) {
                menu = menu.Take(3).ToList();
            }
        }
        private string headerText;
        public Models.Account LoggedInUser { get; set; }
        public void PrintHeader()
        {

            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Startpage menu";
            UserInterface.GUI.PrintMenu(title, menu);
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
