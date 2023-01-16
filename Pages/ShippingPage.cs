using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class ShippingPage : IPage
    {
        private List<string> menu = new List<string>() { "Choose shipping option", "Choose alternative address", "Continue to checkout", "Back to shopping cart" };
        private UserData loggedInUser;

        public ShippingPage(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;
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
                    case 1: //choose shipping option
                        Console.WriteLine("Select your shipping option: ");
                        SetShippingOption();
                        break;
                    case 2:// choose alternative address
                        SetAlternativeShippingAddress();
                        break;
                    case 3: // continue to checkout
                        break;
                    case 4: // back to shoppingcart
                        return true;
                }

            }
            return false;
        }

        public void PrintHeader()
        {
            var items = loggedInUser.ProductsInShoppingCart();
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            for (int index = 0; index < items.Count; index++)
            {
                strings[index] += " Unit Cost | total amount : " + items[index].OrderDetails.First().Quantity;
            }
            strings.Add(loggedInUser.GetSummary(true, true));
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
            Console.WriteLine(loggedInUser.ShoppingCart.ShippingOption);
        }

        public void PrintMenu()
        {
            string title = "Customize your shipping: ";
            GUI.PrintMenu(title, menu);

        }
        public void PrintFooter()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.SetCursorPosition(0, 15);
            Console.WriteLine(loggedInUser.GetSummary());
            Console.ResetColor();
            //Console.SetCursorPosition(0, 0);
        }
        private void SetShippingOption()
        {
            var names = Enum.GetNames(typeof(ShippingOption)).ToList();
            for (ShippingOption option = ShippingOption.Tortoise; option <= ShippingOption.African_Swallow; option++) {
                names[(int)option-1] += (" - " + ItemSelector<Person>.GetShippingCost(option) + "§");
            }
            GUI.PrintMenu("Shipping options", names);
            int input = InputModule.GetIntInRange(0, names.Count);
            loggedInUser.ShoppingCart.ShippingOption = (ShippingOption)input;
        }
        private void SetAlternativeShippingAddress()
        {
            Console.Write("Please enter alternative adress: ");
            string newAddress = InputModule.GetString();
            loggedInUser.ShoppingCart.ShippingAdress = newAddress;
            Console.Write("Please enter alternative postal code: ");
            int newPostalCode = InputModule.GetInt();
            loggedInUser.ShoppingCart.ShippingPostalcode = newPostalCode;
            Console.Write("Please enter alternative city: ");
            string newCity = InputModule.GetString();
            loggedInUser.ShoppingCart.ShippingCity = newCity;
            Console.Write("Please enter alternative country: ");
            string newCountry = InputModule.GetString();
            loggedInUser.ShoppingCart.ShippingCountry = newCountry;

        }
    }
}