using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data
{
    public static class CategoryManager
    {
            public static List<Category> Categories { get; set; }

            public static List<Category> GetAllCategories()
            {
                if (Categories == null || !Categories.Any())
                {
                Categories = new List<Category>() { 
                    new Category() { CategoryName = "T-shirts", Description = "Shirts with short sleeves" },
                   new Category() { CategoryName = "", Description = "" },
                      new Category() { CategoryName = "", Description = "" },
                      new Category() { CategoryName = "", Description = "" },
                         new Category() { CategoryName = "", Description = "" },
                };
                }

                return Categories;
            }
        }
    }

