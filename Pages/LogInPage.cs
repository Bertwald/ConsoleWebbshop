using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class LogInPage
    {      
        public LogInPage()
        {
            headerText = "Username and password:";
        }
        private string headerText;
        public Models.Person? LoggedInUser { get; set; }
        public void PrintHeader()
        {
            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            
        }
        public bool Run()
        {
            while (true)
            {
                Console.Write("Enter username: ");
                string userName = InputModule.GetString();
                Console.Write("Enter password: ");
                string passWord = InputModule.GetString();

                Models.Account account = Logic.Validation.ValidateUser(userName, passWord);

            }
            //return new StartPage().Run();
        }

        public void PrintFooter()
        {

        }
    }
}
