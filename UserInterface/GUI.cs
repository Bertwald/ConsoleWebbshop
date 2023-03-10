using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.UserInterface
{
    internal class GUI
    {
        internal static void ClearWindow() {
            Console.Clear();
        }

        internal static void Delay() {
            Console.WriteLine("Press ANY key to continue");
            Console.ReadKey(true);
        }

        //printmetoder
        internal static void PrintHeader(List<string> headerText)
        {
            foreach (string text in headerText)
            {
                Console.WriteLine(text);
            }
        }

        internal static void PrintMenu(string title, List<string> menu)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine("---------------------------");

            for (int row = 0; row < menu.Count; row++)
            {
                Console.WriteLine($"[{row+1}] " + menu[row]);
            }
        }

        internal static void PrintSelectedProduct(Product? chosen)
        {
            Console.WriteLine($"Name: {chosen.Name}");
            Console.WriteLine();
            Console.WriteLine($"Description: {chosen.Description}");
            Console.WriteLine();
            Console.WriteLine($"LongDescription: {chosen.LongDescription}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Price: " + chosen.Price * (1d + chosen.Vat/100)+"§");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("UnitsInStock: " + chosen.UnitsInStock);
            Console.WriteLine();
            if (chosen.Categories.Any()) {
                Console.Write("In Categories: ");
                foreach (Category cat in chosen.Categories) {
                    Console.Write(cat.CategoryName);
                }
                Console.WriteLine();
            }

        }

        internal static void PrintSelectedProducts<T>(List<T> products, string text = "") where T : IPrintable  
        {
            Console.WriteLine();
            Console.WriteLine(text);
            foreach (var p in products.Take(3))
            {
                p.Print();
                Console.SetCursorPosition(Console.GetCursorPosition().Left + 20, Console.GetCursorPosition().Top - 1);
            }
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top +2);
        }

        internal static void SetWindowTitle() {
            Console.Title = "The shops who say Ni!";
        }

        internal static void ShowShoppingCartItems(List<string> shoppingCart)
        {
            foreach (var product in shoppingCart)
            {
                Console.WriteLine(product);
            }
        }
    }
}
