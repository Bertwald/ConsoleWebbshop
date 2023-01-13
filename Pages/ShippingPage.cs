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
            List<string> strings = new();
            strings.AddRange(loggedInUser.ProductsAsStrings());
            for (int index = 0; index < details.Count; index++)
            {
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
        private void SetShippingOption()
        {
            var names = Enum.GetNames(typeof(ShippingOption)).ToList();
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
            Console.WriteLine("Please enter alternative country: ");
            string newCountry = InputModule.GetString();
            loggedInUser.ShoppingCart.ShippingCountry = newCountry;
        }
    }
}