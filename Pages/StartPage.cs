using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Pages
{
    internal class StartPage : IPage
    {
        public StartPage(Models.Person user)
        {
            LoggedInUser = user;
            headerText = "Welcome customer " + LoggedInUser.FirstName + " " + LoggedInUser.LastName;
        }
        private string headerText;
        public Models.Person LoggedInUser { get; set; }
        public void PrintHeader()
        {

            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Startpage menu";
            List<string> menu = new()
            {
                "Choose category",
                "Search for products",
                "Log in / log out",
                "Account information",
                "Show shopping cart"
            };
            UserInterface.GUI.PrintMenu(title, menu);
        }
        public IPage GetNextPage()
        {
            throw new NotImplementedException();
        }

        public void PrintFooter()
        {
            throw new NotImplementedException();
        }


    }
}
