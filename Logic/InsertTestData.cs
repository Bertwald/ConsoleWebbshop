using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestWebbshopCodeFirst.Data;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic {
    public class InsertTestData {
        internal static void InsertData() {

            using (var shopDb = new OurDbContext()) {
                List<Person> users = UserManager.GetAllUsers();
                List<Employee> employees = EmployeeManager.GetAllEmployees();
                List<Customer> customers = CustomerManager.GetAllCustomers();
                List<Category> categories = CategoryManager.GetAllCategories();
                List<Product> products = ProductManager.GetAllProducts();
                List<int> categoryIds = ProductManager.GetCategoryIndices();

                for (int productIndex = 0; productIndex < products.Count; productIndex++) {
                    products[productIndex].Categories.Add(categories[categoryIds[productIndex]]);
                    categories[categoryIds[productIndex]].Products.Add(products[productIndex]);
                }
                shopDb.AddRange(users);
                shopDb.AddRange(categories);
                shopDb.AddRange(products);
                shopDb.SaveChanges();
                shopDb.AddRange(employees);
                shopDb.AddRange(customers);

                foreach (Employee employee in employees) {
                    employee.Person.Employees.Add(employee);
                }
                foreach (Customer customer in customers) {
                    customer.Person.Customers.Add(customer);
                }
                shopDb.SaveChanges();
            }
        }

        internal static void SetRandomChosen() {
            using (var shopDb = new OurDbContext()) {
                var products = shopDb.Products.ToList();
                var categories = shopDb.Categories.ToList();
                //shopDb.Add(new Category() { CategoryName = "Selected", Description = "Recommended products" });
                //shopDb.SaveChanges();
                for (int index = 0; index < 3; index++) {
                    Random random = new Random();
                    int productIndex = random.Next(0, products.Count);
                    products[productIndex].Categories.Add(categories[categories.Count - 1]);
                    categories[categories.Count - 1].Products.Add(products[productIndex]);
                }
                foreach (Category category in categories) {
                    shopDb.Update(category);
                }
                foreach (Product product in products) {
                    shopDb.Update(product);
                }
                shopDb.SaveChanges();
            }
        }
        internal static void CreateUserAccounts() {
            using (var db = new OurDbContext()) {
                //Generate CustomerAccounts
                var users = db.Persons.Where(x => x.Customers.Any() && !x.Accounts.Any()).ToList();
                List<Account> customers = AccountGenerator.GenerateAccountsFor(users, Privilege.Customer);
                //Generate boss accounts
                users = db.Persons.Where(x => x.Employees.Any() && !x.Accounts.Any()).ToList();
                List<Account> admins = AccountGenerator.GenerateAccountsFor(users, Privilege.Admin);
                Account user = new() {
                    Username = "Cust",
                    Password = "Cust",
                    Privilege = Privilege.Customer,
                    User = db.Persons
                    .Where(x => x.FirstName == "Christina" && x.LastName == "Holm")
                    .First()
                };
                Account admin = new() {
                    Username = "Admin",
                    Password = "Admin",
                    Privilege = Privilege.Admin,
                    User = db.Persons
                    .Where(x => x.FirstName == "Christina" && x.LastName == "Holm")
                    .First()
                };
                customers.Add(user);
                customers.Add(admin);

                db.AddRange(customers);
                db.AddRange(admins);
                db.SaveChanges();
            }
        }

    }
}
