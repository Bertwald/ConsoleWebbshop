﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class CheckOutPage : IPage
    {
        private UserData loggedInUser;
        private List<string> menu = new()
            {
                "Choose payment option",
                "Confirm purchase",
                "Back to shipping",
                "Back to start page"
            };

        public CheckOutPage(UserData user)
        {
            this.loggedInUser = user;
        }
        private string headerText;
        public void PrintHeader()
        {
            var items = loggedInUser.ProductsInShoppingCart();
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            for (int index = 0; index < items.Count; index++)
            {
                strings[index] += " § | total amount : " + loggedInUser.ShoppingCart.OrderDetails.First().Quantity;
            }
            //strings.Add(loggedInUser.GetSummary(true, true));
            GUI.ShowShoppingCartItems(strings);

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Your billing address: ");
            loggedInUser.Person.Print();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------");
            if (loggedInUser.ShoppingCart.ShippingAdress != null)
            {
                Console.WriteLine("Alternative delivery address: ");
                Console.WriteLine(loggedInUser.ShoppingCart.GetAlternativeAddress());
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------"); ;
            if (loggedInUser.ShoppingCart.ShippingOption == 0)
            {
                loggedInUser.ShoppingCart.ShippingOption = (ShippingOption)1;
            }
            Console.WriteLine("Your shipping option:");
            Console.WriteLine(loggedInUser.ShoppingCart.ShippingOption + " - " + ItemSelector<Person>.GetShippingCost(loggedInUser.ShoppingCart.ShippingOption) + " §");
            if (loggedInUser.ShoppingCart.PayingOption == 0)
            {
                loggedInUser.ShoppingCart.PayingOption = (PayingOption)1;
            }
            Console.WriteLine("Your payment option:");
            Console.WriteLine(loggedInUser.ShoppingCart.PayingOption);

        }
        public void PrintMenu()
        {
            string title = "Payment menu";

            UserInterface.GUI.PrintMenu(title, menu);
        }
        public bool Run()
        {
            bool exit = false;
            while (!exit)
            {
                GUI.ClearWindow();
                PrintHeader();
                PrintMenu();
                PrintFooter();
                int choice = InputModule.SelectFromList(menu);

                switch (choice)
                {
                    case 1: //choose payment
                        SetPayingOption();
                        break;
                    case 2: // confirm purchase
                        exit = new ConfirmedPurchasePage(loggedInUser).Run();
                        break;
                    case 3: //back one step

                        break;
                    case 4: //back to start page
                        return true;
                }
            }
            return true;
        }


        public void PrintFooter()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.SetCursorPosition(0, 15);
            Console.WriteLine(loggedInUser.GetSummary(true, true));
            Console.ResetColor();
            //Console.SetCursorPosition(0, 0);
        }
        private void SetPayingOption()
        {
            var names = Enum.GetNames(typeof(PayingOption)).ToList();           
            GUI.PrintMenu("Paying options", names);
            int input = InputModule.GetIntInRange(0, names.Count);
            loggedInUser.ShoppingCart.PayingOption = (PayingOption)input;
        }
    }
}
