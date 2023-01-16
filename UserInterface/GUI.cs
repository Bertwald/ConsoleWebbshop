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
            Console.WriteLine($"{chosen.Name}");
            Console.WriteLine();
            Console.WriteLine(chosen.Description);
            Console.WriteLine();
            Console.WriteLine(chosen.LongDescription);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Price: " + chosen.Price);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Units in stock: " + chosen.UnitsInStock);
           
        }

        internal static void PrintSelectedProducts<T>(List<T> products, string text = "") where T : IPrintable  
        {
            Console.WriteLine();
            Console.WriteLine(text);
            foreach (var p in products)
            {
                //TODO: Fix pretty print
                //p.Print();

                Console.Write(p.ToString() + "\t\t"); 
            }
        }

        internal static void SetWindowTitle<T>(T instance, Privilege privilege = Privilege.Visitor) {
            Console.Title = instance.GetType().Name + " AS " + privilege;
        }

        internal static void ShowShoppingCartItems(List<string> shoppingCart)
        {
            foreach (var product in shoppingCart)
            {
                Console.WriteLine(product);
            }
            //GUI.Delay();
        }
    }
}
