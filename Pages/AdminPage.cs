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
            LoggedInUser = user.FirstName + " " + user.LastName;
            headerText = "Welcome admin " + LoggedInUser;
        }  
        private string headerText;
        public string LoggedInUser { get; set; }
        public void PrintHeader()
        {

        }
        public void PrintMenu()
        {
        }
        public IPage GetNextPage()
        {
            return new AdminPage();
        }

        public void PrintFooter()
        {
        }


    }
}
