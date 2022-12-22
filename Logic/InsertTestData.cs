using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Data;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic {
    public class InsertTestData {
        internal static void InsertData() {

            using (var shopDb = new OurDbContext()) {
                List<User> users = UserManager.GetAllUsers();
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
                foreach(Category category in categories) {
                    shopDb.Update(category);
                }
                foreach(Product product in products) {
                    shopDb.Update(product);
                }
                shopDb.SaveChanges();
            }
        }

    }
}
