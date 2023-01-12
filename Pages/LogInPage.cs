using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
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

                UserData? acp = Logic.Validation.ValidateUser(userName, passWord);

                if(acp == null) {
                    return false;
                }
                if(acp.Privilege == Logic.Privilege.Customer) {
                    return new CustomerPage(acp).Run();
                } else { // Privilege.Admin
                    return new AdminPage(acp).Run();
                }
            }
        }

        public void PrintFooter()
        {

        }
    }
}
