using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;
using TestWebbshopCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using TestWebbshopCodeFirst.Logic;

namespace TestWebbshopCodeFirst.Pages
{
    internal class CustomerPage : IPage
    {
        private List<string> menu = new()
            {
                "Choose category",
                "Search for products",
                "Log in / log out",
                "Account information",
                "Show shopping cart"
            };
        private List<Product> selectedProducts = new();

        private string headerText;
        private readonly int numberOfSelectedToDisplay = 3;

        public Account LoggedInUser { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerPage(Account user)
        {
            LoggedInUser = user;
            SetHeaderText();
            if (user.Privilege == Logic.Privilege.Visitor)
            {
                menu = menu.Take(3).ToList();
            }
            RetrieveSelectedItems();
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private void SetHeaderText()
        {
            headerText = LoggedInUser.Privilege == Logic.Privilege.Visitor ?
                "Welcome Honored Guest" + Environment.NewLine +
                "Feel free to browse out exquisite inventory!" + Environment.NewLine + "When you decide to buy one or many of our excellent " +
                "products" + Environment.NewLine + "You will need a registered account to purchase said items from our collection of fine merchandise" :
                "Welcome back dear " + LoggedInUser.Privilege + " " + LoggedInUser.Username + Environment.NewLine +
                "We eagerly await your next order";
        }

        private void RetrieveSelectedItems()
        {
            using (var db = new OurDbContext())
            {
                selectedProducts = db.Products
                                     .Where(p => p.Categories
                                                  .Where(category => category.Id == 10)
                                                  .Count() > 0)
                                     .Take(numberOfSelectedToDisplay)
                                     .ToList();
            }
        }

        public void PrintHeader()
        {
            GUI.PrintHeader(new List<string> { headerText });
            GUI.PrintSelectedProducts(selectedProducts);
        }
        public void PrintMenu()
        {
            string title = "Customer Shopping menu";
            GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            GUI.ClearWindow();
            GUI.SetWindowTitle(this, LoggedInUser.Privilege);
            PrintHeader();
            PrintMenu();
            PrintFooter();
            int choice = InputModule.SelectFromList(menu);

            switch (choice)
            {
                case 1:
                    using (var db = new OurDbContext())
                    {
                        List<Category> categories = db.Categories.ToList();
                        Category? category = ItemSelector<Category>.GetItemFromList(categories);
                        List<Product> products = db.Products.Where(x => x.Categories.Contains(category)).ToList();
                        Product? product = ItemSelector<Product>.GetItemFromList(products);
                    }
                    break;
                case 2:
                    using (var db = new OurDbContext())
                    {
                        Console.Write("Search: ");
                        var search = InputModule.GetString();
                        List<Product> products = db.Products
                                                .Where(x => x.Name
                                                    .Contains(search) || x.Description
                                                    .Contains(search) || x.LongDescription
                                                    .Contains(search))
                                                .ToList();
                        Product? product = ItemSelector<Product>.GetItemFromList(products);
                    }
                    break;
                case 3:
                    return true;

                case 4:
                    using (var db = new OurDbContext())
                    {
                        var miniMenu = new List<string> { "Personal details", "Order details" };
                        GUI.PrintMenu("Your account", miniMenu);
                        int menuChoice = InputModule.SelectFromList(miniMenu);

                        switch (menuChoice)
                        {
                            case 1:
                                var personalInformation = db.Persons
                                                .Where(p => p.Id == LoggedInUser.Id)
                                                .Include(p => p.Customers).ToList();

                                ItemSelector<Person>.GetItemFromList(personalInformation);
                                                
                                break;
                            case 2:
                                //Orderdetails
                                break;

                        }



                    }
                    break;

            }
            Console.ReadKey();
            return true;
        }

        public void PrintFooter()
        {
        }


    }
}
