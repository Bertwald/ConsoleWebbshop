using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Pages
{
    internal class AdminPage : IPage
    {
        public AdminPage(Models.User user) 
        {
            LoggedInUser = user;
            headerText = "Welcome admin " + LoggedInUser.FirstName + " " + LoggedInUser.LastName;
        }  
        private string headerText;
        public Models.User LoggedInUser { get; set; }
        public void PrintHeader()
        {
            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Admin deluxe menu";
            List<string> menu = new()
            {
                "Manage products",
                "Manage productcategories",
                "Add product to startpage",
                "Manage users"
            };
            UserInterface.GUI.PrintMenu(title, menu);
        }
        public IPage GetNextPage()
        {
            return new AdminPage(LoggedInUser);
        }

        public void PrintFooter()
        {

        }


    }
}
