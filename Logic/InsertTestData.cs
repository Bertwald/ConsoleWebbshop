using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Data;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Logic
{
    public class InsertTestData
    {
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
                //shopDb.AddRange(users);
                //shopDb.AddRange(categories);
                //shopDb.AddRange(products);
                //shopDb.SaveChanges();
                shopDb.AddRange(employees);
                //shopDb.AddRange(customers);
                shopDb.SaveChanges();
            }
        }

    }
}
