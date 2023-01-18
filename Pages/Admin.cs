using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;
using Dapper;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace TestWebbshopCodeFirst.Pages
{
    internal class Admin
    {

        static readonly string connString = "Server=tcp:grupp2admin.database.windows.net,1433;Initial Catalog=Webshopen;Persist Security Info=False;User ID=grupp2admin;Password=Admingrupp2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string headerText;
        private List<string> menu = new()
            {
                "Manage products",
                "Manage categories",
                "Set Product Categories",
                "Manage users",
                "Statistics",
                "Quit"
            };
        public Admin(UserData user)
        {
            LoggedInUser = user;
            headerText = $"Welcome {LoggedInUser.Privilege}: " + LoggedInUser.Username;
        }
        public UserData LoggedInUser { get; set; }
        public void PrintMenu()
        {
            string title = "Admin deluxe menu";
            UserInterface.GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            while (true)
            {
                GUI.ClearWindow();
                Console.WriteLine(headerText);
                PrintMenu();
                int choice = InputModule.SelectFromList(menu);
                switch (choice)
                {
                    case 1: // "Manage products"
                        ManageProducts();
                        break;
                    case 2: //"Manage categories"
                        ManageCategories();
                        break;
                    case 3: //"Add product to startpage"
                        LinkCategories();
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

        private void ShowStatistics()
        {
            List<string> optionmenu = new(){
                "Low in stock",
                "Surplus stock",
                "Sales per month",
                "Return"
            };
        }

        private void ManageUsers()
        {
            List<string> objectMenu = new List<string>() {
                "Person",
                "Customer",
                "Account",
                "Show Order History",
                "Return"
            };

            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };

            while (true)
            {
                GUI.ClearWindow();
                GUI.PrintMenu("Table to Alter", objectMenu);
                int outerChoice = InputModule.SelectFromList(objectMenu);
                //--------------------------------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------------------------

                switch (outerChoice)
                {
                    case 1: //Person
                        while (true)
                        {

                            GUI.ClearWindow();
                            Person? chosen;
                            GUI.PrintMenu("Action for Table", optionmenu);
                            int choice = InputModule.SelectFromList(optionmenu);
                            switch (choice)
                            {
                                case 1: // Add
                                    GUI.ClearWindow();
                                    Registration.AddPerson();
                                    break;
                                case 2: //Delete
                                    GUI.ClearWindow();
                                    chosen = SelectFromDatabase<Person>();
                                    using (var db = new WebshopDbContext())
                                    {
                                        db.Remove(chosen);
                                        db.SaveChanges();
                                    }
                                    break;
                                case 3: //Alter
                                    GUI.ClearWindow();
                                    chosen = SelectFromDatabase<Person>();
                                    chosen.PrintWithFields();
                                    (string column, string value) = GetColumnValuePair();
                                    AlterItem(chosen.Id, "Persons", column, value);
                                    break;
                                case 4:
                                    return;
                            }
                            break;

                        }
                        break;
                    case 2: // Customer
                        while (true)
                        {
                            GUI.ClearWindow();
                            Customer? chosen;
                            GUI.PrintMenu("Action for Table", new List<string> { "Alter", "Return" });
                            int choice = InputModule.SelectFromList(optionmenu);
                            switch (choice)
                            {
                                case 1: //Alter
                                    GUI.ClearWindow();
                                    chosen = SelectFromDatabase<Customer>();
                                    Console.WriteLine("Old CreditCardNumber: " + chosen.CreditCardNumber);
                                    Console.WriteLine("New value: ");
                                    string value = InputModule.GetString();
                                    AlterItem(chosen.Id, "Customers", "CreditCardNumber", value);
                                    break;
                                case 2:
                                    return;
                            }
                            break;
                        }
                        break;
                    case 3: // Account
                        while (true)
                        {
                            GUI.ClearWindow();
                            Account? chosen;
                            GUI.PrintMenu("Action for Table", new List<string> { "Alter", "Return" });
                            int choice = InputModule.SelectFromList(optionmenu);
                            switch (choice)
                            {
                                case 1: //Alter
                                    GUI.ClearWindow();
                                    chosen = SelectFromDatabase<Account>();
                                    GUI.ClearWindow();
                                    Console.WriteLine(chosen.ToString());
                                    Console.WriteLine("Valid options for Privilege: " + string.Join(", " , Enum.GetNames(typeof(Privilege))));
                                    (string column, string value) = GetColumnValuePair();
                                    if(column == "Privilege") {
                                        int val = (int)Enum.Parse(typeof(Privilege), value);
                                        value = val.ToString();
                                    }
                                    AlterItem(chosen.Id, "Accounts", column, value);
                                    break;
                                case 2:
                                    return;
                            }
                            break;
                        }
                        break;
                    case 4:
                        GUI.ClearWindow();
                        ShowOrderHistory();
                        GUI.Delay();
                        break;

                    case 5: // Return
                        return;
                }
            }

            //--------------------------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------------------------
        }

        private void ShowOrderHistory() {
            Customer cust = SelectFromDatabase<Customer>();
            List<Order> orders = new(); 
            using(var db = new WebshopDbContext()) {
                orders = db.Orders.Include(x => x.OrderDetails).Where(x => x.Custumer == cust).ToList();
            }
            foreach(var order in orders) {
                Console.WriteLine(order.ToString());
                Console.WriteLine(new string('-', 30));
            }
        }

        private void LinkCategories()
        {
            var chosenProduct = SelectFromDatabase<Product>();
            var chosenCategory = SelectFromDatabase<Category>();
            using (var db = new WebshopDbContext())
            {
                db.Attach(chosenProduct);
                chosenProduct.Categories.Add(chosenCategory);
                db.SaveChanges();
            }
        }

        private void ManageCategories()
        {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
            while (true)
            {

                GUI.ClearWindow();
                Category? chosenCategory;
                GUI.PrintMenu("Category menu", optionmenu);
                int choice = InputModule.SelectFromList(optionmenu);
                switch (choice)
                {
                    case 1: // Add
                        GUI.ClearWindow();
                        Console.Write("Name: ");
                        string name = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("Description: ");
                        string description = InputModule.GetString();
                        using (var db = new WebshopDbContext())
                        {
                            db.Add(new Category
                            {
                                CategoryName = name,
                                Description = description
                            });
                            db.SaveChanges();
                        }
                        break;
                    case 2: //Delete
                        GUI.ClearWindow();
                        chosenCategory = SelectFromDatabase<Category>(); //SelectCategoryFromDatabase();
                        Console.WriteLine(chosenCategory.CategoryName);
                        using (var db = new WebshopDbContext())
                        {
                            db.Remove(chosenCategory);
                            db.SaveChanges();
                        }
                        break;
                    case 3: //Alter
                        GUI.ClearWindow();
                        chosenCategory = SelectFromDatabase<Category>();
                        Console.WriteLine($"CategoryName: {chosenCategory.CategoryName} Description: {chosenCategory.Description}");
                        (string column, string value) = GetColumnValuePair();
                        AlterItem(chosenCategory.Id, "categories", column, value);
                        break;
                    case 4:
                        return;
                }
            }
        }

        private void ManageProducts()
        {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
            while (true)
            {

                GUI.ClearWindow();
                Product? chosenProduct;
                GUI.PrintMenu("Product menu", optionmenu);
                int choice = InputModule.SelectFromList(optionmenu);
                switch (choice)
                {
                    case 1: // Add
                        GUI.ClearWindow();
                        Console.Write("Name: ");
                        string name = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("Price (vat excluded): ");
                        string price = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("Vat: ");
                        string vat = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("Stock: ");
                        string stock = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("Description: ");
                        string description = InputModule.GetString();
                        Console.WriteLine();
                        Console.Write("LongDescription: ");
                        string longDescription = InputModule.GetString();
                        List<Category> categories = new();
                        using (var db = new WebshopDbContext())
                        {
                            do
                            {
                                GUI.ClearWindow();
                                List<Category> categoryChoices = (db.Categories.ToList());
                                categoryChoices.Add(new Category { CategoryName = "Break" });
                                categories.Add(ItemSelector<Category>.GetItemFromList(categoryChoices));
                            } while (categories.Last().CategoryName != "Break");
                            categories.RemoveAll(x => x.CategoryName == "Break");
                            db.Add(new Product
                            {
                                Name = name,
                                Price = double.Parse(price),
                                Vat = double.Parse(vat),
                                UnitsInStock = int.Parse(stock),
                                Description = description,
                                LongDescription = longDescription,
                                Categories = categories
                            });
                            db.SaveChanges();
                        }

                        break;
                    case 2: //Delete
                        GUI.ClearWindow();
                        chosenProduct = SelectFromDatabase<Product>();
                        GUI.PrintSelectedProduct(chosenProduct);
                        using (var db = new WebshopDbContext())
                        {
                            db.Remove(chosenProduct);
                            db.SaveChanges();
                        }
                        break;
                    case 3: //Alter
                        GUI.ClearWindow();
                        chosenProduct = SelectFromDatabase<Product>();
                        GUI.PrintSelectedProduct(chosenProduct);
                        (string column, string value) = GetColumnValuePair();
                        AlterItem(chosenProduct.Id, "products", column, value);
                        break;
                    case 4:
                        return;
                }
            }
        }




        private static T? SelectFromDatabase<T>() where T : class
        {
            List<T> objects;
            using (var db = new WebshopDbContext())
            {
                objects = db.Set<T>().ToList();
            }
            T? chosen = ItemSelector<T>.GetItemFromList(objects);
            return chosen;
        }

        private static (string, string) GetColumnValuePair()
        {
            Console.Write("Choose a column to alter: ");
            string column = InputModule.GetString();
            Console.Write("Enter the new value: ");
            string value = InputModule.GetString();
            return (column, value);
        }

        public static bool AlterItem(int id, string tableName, string column, string newValue)
        {
            int affectedRow = 0;

            string sql = $"UPDATE [{tableName}] SET [{column}] = '{newValue}' WHERE Id = {id}";

            using (var connection = new SqlConnection(connString))
            {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow > 0;
        }



    }
}
