using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class Login
    {      
        public Login()
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
            bool exit = false;
            while (!exit)
            {
                GUI.ClearWindow();
                Console.Write("Enter username: ");
                string userName = InputModule.GetString();
                Console.Write("Enter password: ");
                string passWord = InputModule.GetString();

                UserData? acp = Logic.Validator.ValidateUser(userName, passWord);

                if(acp == null) {
                    return false;
                }
                if(acp.Privilege == Logic.Privilege.Customer) {
                    exit = new CustomerHome(acp).Run();
                } else { // Privilege.Admin
                    exit = new Admin(acp).Run();
                }
            }
            return false;
        }

        public void PrintFooter()
        {

        }
    }
}
