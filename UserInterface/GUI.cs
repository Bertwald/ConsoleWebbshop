﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
