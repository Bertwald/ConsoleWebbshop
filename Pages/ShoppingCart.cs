using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class ShoppingCart : IPage
    {
        private List<string> menu = new List<string>() { "Change amount", "Remove item", "Back one step", "Back to Customer Page", "Go to shipping option" };
        private UserData loggedInUser;
        private List<OrderDetail> details = new();

        public ShoppingCart(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;

            foreach (var product in loggedInUser.ProductsInShoppingCart())
            {
                var newDetail = new OrderDetail()
                {
                    Order = loggedInUser.ShoppingCart,
                    Product = product,
                    Quantity = Math.Min(product.UnitsInStock, 1),
                    UnitPrice = product.Price * (1d + product.Vat / 100)
                };
                details.Add(newDetail);
            }
        }
        private void CleanOrderDetails()
        {
            foreach (var product in loggedInUser.ShoppingCart.Products)
            {
                product.OrderDetails.Clear();
            }
            details.Clear();
            loggedInUser.ShoppingCart.OrderDetails.Clear();
        }
        public bool Run()
        {
            bool exit = false;
            while (!exit)
            {
                if (!loggedInUser.ShoppingCart.Products.Any())
                {
                    CleanOrderDetails();
                    return false;
                }
                loggedInUser.ShoppingCart.OrderDetails = details;
                Console.Clear();
                PrintHeader();
                PrintMenu();
                PrintFooter();


                int choice = InputModule.SelectFromList(menu);

                switch (choice)
                {
                    case 1: //change amount
                        Console.WriteLine("Choose index to change quantity");
                        GUI.PrintMenu("Change quantity for:", details.Select(x => $"{x.Product.Name} - {x.Product.Description}").ToList());
                        int index = InputModule.SelectFromList(details.Select(x => " ").ToList()) - 1;
                        OrderDetail chosenDetail = details[index];
                        Console.WriteLine("+/- to change quantity");
                        ConsoleKey key = Console.ReadKey().Key;
                        if (key == ConsoleKey.Add || key == ConsoleKey.OemPlus)
                        {
                            chosenDetail.Quantity = Math.Min(details[index].Product.UnitsInStock, chosenDetail.Quantity + 1);
                        }
                        else if (key == ConsoleKey.Subtract || key == ConsoleKey.OemMinus)
                        {
                            chosenDetail.Quantity = Math.Max(0, chosenDetail.Quantity - 1);
                        }
                        break;
                    case 2:// delete item
                        Console.WriteLine("Choose to delete");
                        //GUI.PrintMenu("Delete at:", loggedInUser.ShoppingCart.Products.Select(x => $"{x.Name} - {x.Description}").ToList());
                        GUI.PrintMenu("Delete at:", details.Select(x => $"{x.Product.Name} - {x.Product.Description}").ToList());
                        int delete = InputModule.SelectFromList(details.Select(x => " ").ToList()) - 1;
                        details.RemoveAt(delete);                     
                        loggedInUser.ShoppingCart.Products.RemoveAt(delete);
                        break;
                    case 3: // back one step
                        CleanOrderDetails();
                        return false;
                    case 4: // back to startpage
                        CleanOrderDetails();
                        return true;
                    case 5: // continue to checkout
                        if (loggedInUser.ShoppingCart.OrderDetails.Select(x => x.Quantity).Max() < 1)
                        {
                             break;
                        }
                        exit = new Shipping(loggedInUser).Run();
                        break;
                }

            }
            CleanOrderDetails();
            return true;
        }

        public void PrintHeader()
        {
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            for (int index = 0; index < details.Count; index++)
            {
                strings[index] += " § | total amount : " + details[index].Quantity /*+ " | Sum: " + loggedInUser.ProductsInShoppingCart()[index].Price * details[index].Quantity*/;
            }
            GUI.ShowShoppingCartItems(strings);
        }

        public void PrintMenu()
        {
            string title = "Please take a look at your shopping cart and then make a choice: ";
            GUI.PrintMenu(title, menu);

        }
        public void PrintFooter()
        {
            Console.WriteLine("----------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(loggedInUser.GetSummary(true));
            Console.ResetColor();

        }
    }
}
