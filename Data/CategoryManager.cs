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
                    new Category() { CategoryName = "T-shirt", Description = "Shirt with short sleeves" },
                    new Category() { CategoryName = "Trouser", Description = "A garment that covers the lower half of the body" },
                    new Category() { CategoryName = "Sweater", Description = "A piece of clothing typically with long sleeves" },
                    new Category() { CategoryName = "Skirt", Description = "The lower part of a dress or a separate outer garment that covers a person from the waist downwards" },
                    new Category() { CategoryName = "Coat", Description = "A wide robe worn over ones clothes to protect from cold or rain" },
                    new Category() { CategoryName = "Headwear", Description = "Something to put on your head for style or comfort." },
                    new Category() { CategoryName = "Footwear", Description = "Something to put on your feet for style or comfort." },
                    new Category() { CategoryName = "Shirts", Description = "Something to put on your feet for style or comfort." },
                    new Category() { CategoryName = "Accessories", Description = "No outfit is complete and personalized without these." },
                };
                }

                return Categories;
            }
        }
    }

