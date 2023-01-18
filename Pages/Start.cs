using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class Start : IPage
    {
        private List<string> menu = new()
            {
                "Log in",
                "New customer(register)",
                "Continue as visitor",
                "Quit"
            };

        public Start()
        {
            headerText = "Welcome to the shop";
        }
        private string headerText;
        public Person? LoggedInUser { get; set; }
        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Start menu";

            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            GUI.SetWindowTitle();
            bool exit = false;
            while (!exit)
            {
                GUI.ClearWindow();
                PrintHeader();
                PrintMenu();
                PrintFooter();
                int choice = InputModule.SelectFromList(menu);

                switch (choice)
                {
                    case 1:
                        exit = new Login().Run();
                        break;
                    case 2:
                        exit = new Registration().Run();
                        break;
                    case 3:
                        //visitor
                        UserData visitor = new UserData(new Account() { Username = "Sneaky Weasel" }, new Person(), new Customer());
                        exit = new CustomerHome(visitor).Run();
                        break;
                    case 4:
                        return true;
                }
            }
            return true;
        }


        public void PrintFooter()
        {

        }
    }
}
