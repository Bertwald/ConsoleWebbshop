using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages {
    internal class AdminPage {
        private List<string> menu = new()
            {
                "Manage products",
                "Manage categories",
                "Add product to startpage",
                "Manage users",
                "Statistics",
                "Quit"
            };
        public AdminPage(UserData user) {
            LoggedInUser = user;
            //headerText = $"Welcome {LoggedInUser.Privilege}: " + LoggedInUser.Username;
        }
        public UserData LoggedInUser { get; set; }
        public void PrintMenu() {
            string title = "Admin deluxe menu";
            UserInterface.GUI.PrintMenu(title, menu);
        }
        public bool Run() {
            while (true) {
                GUI.ClearWindow();
                PrintMenu();
                int choice = InputModule.SelectFromList(menu);
                switch (choice) {
                    case 1: // "Manage products"
                        ManageProducts();
                        break;
                    case 2: //"Manage categories"
                        ManageCategories();
                        break;
                    case 3: //"Add product to startpage"
                        AddToStartPage();
                        break;
                    case 4: //"Manage users"
                        ManageUsers();
                        break;
                    case 5: //Statistics
                        ShowStatistics();
                        break;
                    case 6: //Quit
                        return true;
                }

            }
        }

        private void ShowStatistics() {
            List<string> optionmenu = new(){
                "Low in stock",
                "Surplus stock",
                "Sales per month",
                "Return"
            };
        }

        private void ManageUsers() {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
        }

        private void AddToStartPage() {
            //Select from category
            //select product
            //set new category for product
        }

        private void ManageCategories() {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
        }

        private void ManageProducts() {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
        }


    }
}
