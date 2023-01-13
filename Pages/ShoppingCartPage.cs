using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class ShoppingCartPage : IPage
    {
        private List<string> menu = new List<string>() { "Change amount", "Remove item", "Back one step", "Back to Customer Page", "Go to checkout" };
        private UserData loggedInUser;
        private List<OrderDetail> details = new();
        
        public ShoppingCartPage(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;

            foreach (var product in loggedInUser.ProductsInShoppingCart()) {
                var newDetail = new OrderDetail() {
                    Order = loggedInUser.ShoppingCart,
                    Product = product,
                    Quantity = 1,
                    UnitPrice = product.Price
                };
                details.Add(newDetail);
                loggedInUser.ShoppingCart.OrderDetails.Add(newDetail);
            }
        }
        private void CleanOrderDetails() {
            details.Clear();
            loggedInUser.ShoppingCart.OrderDetails.Clear();
        }
        public bool Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintHeader();               
                PrintMenu();
                PrintFooter();

                int choice = InputModule.SelectFromList(menu);

                switch (choice)
                {
                    case 1: //change amount
                        Console.WriteLine("Choose index to change quantity");
                        int index = InputModule.SelectFromList(menu) -1;
                        OrderDetail chosenDetail = details[index];
                        Console.WriteLine("+/- to change quantity");
                        ConsoleKey key = Console.ReadKey().Key;
                        if(key == ConsoleKey.Add || key == ConsoleKey.OemPlus) {
                            chosenDetail.Quantity = Math.Min( loggedInUser.ShoppingCart.Products[index].UnitsInStock, chosenDetail.Quantity+1);
                        } else if (key == ConsoleKey.Subtract || key == ConsoleKey.OemMinus) {
                            chosenDetail.Quantity = Math.Max(0,chosenDetail.Quantity-1);
                        }
                        break;
                    case 2:// delete item
                        int delete = InputModule.SelectFromList(menu) - 1;
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
                        exit = new CheckOutPage().Run();
                        break;
                }

            }
                return false;
        }

        public void PrintHeader()
        {
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            for(int index = 0; index < details.Count; index++) {
                strings[index] += " Unit Cost | total amount : " + details[index].Quantity + " | Sum: " + loggedInUser.ProductsInShoppingCart()[index].Price * details[index].Quantity;
            }
            strings.Add(loggedInUser.GetSummary());
            GUI.ShowShoppingCartItems(strings);
        }

        public void PrintMenu()
        {
            string title = "Please take a look at your shopping cart and then make a choice: ";
            GUI.PrintMenu(title, menu);

        }
        public void PrintFooter()
        {

        }
    }
}
