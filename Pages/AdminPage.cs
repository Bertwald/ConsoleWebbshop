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

namespace TestWebbshopCodeFirst.Pages {
    internal class AdminPage {

        static readonly string connString = "data source=.\\SQLEXPRESS; initial catalog=TestWebbshopCodeFirst; persist security info=True; Integrated Security=True";

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
                        using (var db = new OurDbContext())
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
                        chosenCategory = SelectCategoryFromDatabase();
                        Console.WriteLine(chosenCategory.CategoryName);
                        using (var db = new OurDbContext())
                        {
                            db.Remove(chosenCategory);
                            db.SaveChanges();
                        }
                        break;
                    case 3: //Alter
                        GUI.ClearWindow();
                        chosenCategory = SelectCategoryFromDatabase();
                        Console.WriteLine($"CategoryName: {chosenCategory.CategoryName} Description: {chosenCategory.Description}");
                        (string column, string value) = GetColumnValuePair();
                        AlterItem(chosenCategory.Id, "categories", column, value);
                        break;
                    case 4:
                        return;
                }
            }
        }

        private void ManageProducts() {
            List<string> optionmenu = new(){
                "Add",
                "Delete",
                "Alter",
                "Return"
            };
            while (true) {

                GUI.ClearWindow();
                Product? chosenProduct;
                GUI.PrintMenu("Product menu", optionmenu);
                int choice = InputModule.SelectFromList(optionmenu);
                switch (choice) {
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
                        using (var db = new OurDbContext()) {
                            do {
                                GUI.ClearWindow();
                                List<Category> categoryChoices = (db.Categories.ToList());
                                categoryChoices.Add(new Category { CategoryName = "Break" });
                                categories.Add(ItemSelector<Category>.GetItemFromList(categoryChoices));
                            } while (categories.Last().CategoryName != "Break");
                            categories.RemoveAll(x => x.CategoryName == "Break");
                            db.Add(new Product {
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
                        chosenProduct = SelectProductFromDatabase();
                        GUI.PrintSelectedProduct(chosenProduct);
                        using (var db = new OurDbContext()) {
                            db.Remove(chosenProduct);
                            db.SaveChanges();
                        }
                        break;
                    case 3: //Alter
                        GUI.ClearWindow();
                        chosenProduct = SelectProductFromDatabase();
                        GUI.PrintSelectedProduct(chosenProduct);
                        (string column, string value) = GetColumnValuePair();
                        AlterItem(chosenProduct.Id, "products", column, value);
                        break;
                    case 4:
                        return;
                }
            }
        }


        private static Category? SelectCategoryFromDatabase()
        {
            List<Category> categories;
            using (var db = new OurDbContext())
            {
                categories = db.Categories.ToList();
            }
            Category chosenCategory = ItemSelector<Category>.GetItemFromList(categories);
            return chosenCategory;
        }
        private static Product? SelectProductFromDatabase() {
            List<Product> products;
            using (var db = new OurDbContext()) {
                products = db.Products.ToList();
            }
            Product chosenProduct = ItemSelector<Product>.GetItemFromList(products);
            return chosenProduct;
        }

        private static (string, string) GetColumnValuePair() {
            Console.Write("Choose a column to alter: ");
            string column = InputModule.GetString();
            Console.Write("Enter the new value: ");
            string value = InputModule.GetString();
            return (column, value);
        }

        public static bool AlterItem(int id, string tableName, string column, string newValue) {
            int affectedRow = 0;

            string sql = $"UPDATE [{tableName}] SET [{column}] = '{newValue}' WHERE Id = {id}";

            using (var connection = new SqlConnection(connString)) {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow > 0;
        }



    }
}
