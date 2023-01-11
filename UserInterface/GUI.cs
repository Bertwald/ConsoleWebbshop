using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;

namespace TestWebbshopCodeFirst.UserInterface
{
    internal class GUI
    {
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
            Console.WriteLine(title);
            Console.WriteLine("---------------------------");

            for (int row = 0; row < menu.Count; row++)
            {
                Console.WriteLine($"[{row+1}] " + menu[row]);
            }
        }

        internal static void PrintSelectedProducts<T>(List<T> products) where T : IPrintable  
        {
            foreach (var p in products)
            {
                Console.Write(p.ToString() + "\t\t"); 
            }
        }
    }
}
