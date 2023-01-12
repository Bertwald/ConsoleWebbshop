﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class AdminPage : IPage
    {
        private List<string> menu = new()
            {
                "Manage products",
                "Manage productcategories",
                "Add product to startpage",
                "Manage users"
            };
        public AdminPage(UserData user) 
        {
            LoggedInUser = user;          
            headerText = $"Welcome {LoggedInUser.Privilege}: " + LoggedInUser.Username;
        }  
        private string headerText;
        public UserData LoggedInUser { get; set; }
        public void PrintHeader()
        {
            UserInterface.GUI.PrintHeader(new List<string> { headerText });
        }
        public void PrintMenu()
        {
            string title = "Admin deluxe menu";
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
