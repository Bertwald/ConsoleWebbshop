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
    internal class ShoppingCartPage : IPage
    {
        private List<string> menu = new List<string>() { "Change amount", "Remove item", "Back one step", "Back to Customer Page", "Go to checkout" };
        private UserData loggedInUser;
        
        public ShoppingCartPage(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }
        public bool Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintHeader();               
                PrintMenu();
                PrintFooter();

                int choice = InputModule.SelectFromList(menu);

                switch (choice)
                {
                    case 1: //change amount

                        return false;
                    case 2:// delete item
                        return true;
                    case 3: //go to checkout
                        break;
                    case 4: //back one step
                        break;
                    case 5: //back to startpage
                        break;
                }

            }
                return false;
        }

        public void PrintHeader()
        {
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            strings.Add(loggedInUser.GetSummary());
            GUI.ShowShoppingCartItems(strings);
        }

        public void PrintMenu()
        {
            string title = "Please take a look at your shopping cart and then make a choice: ";
            GUI.PrintMenu(title, menu);

        }
        public void PrintFooter()
        {

        }
    }
}
