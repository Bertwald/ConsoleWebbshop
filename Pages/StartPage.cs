using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class StartPage : IPage
    {
        private List<string> menu = new()
            {
                "Log in",
                "New customer(register)",
                "Continue as visitor",
                "Quit"
            };

        public StartPage()
        {           
            headerText = "Welcome to the shop";
        }
        private string headerText;
        public Models.Person? LoggedInUser { get; set; }
        public void PrintHeader()
        {
            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Start menu";
            
            UserInterface.GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            bool exit = false;
            PrintHeader();
            PrintMenu();
            PrintFooter();
            while (true)
            {
                if (exit) {
                    return true;
                }
                int choice = InputModule.SelectFromList(menu);

                switch(choice)
                {
                    case 1:
                        exit = new LogInPage().Run();
                        break;
                        case 2:
                        exit = new NewCustomer().Run();
                        break;
                        case 3:
                        //visitor
                        Account visitor = new Account() { Username = "Just browsing clothes"};
                        exit = new CustomerPage(visitor).Run();
                        break;
                        case 4:
                        return true;
                        

                }
            }
            //return new StartPage().Run();
        }


        public void PrintFooter()
        {

        }
    }
}
