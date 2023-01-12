using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages {
    internal class DetailedProductPage :IPage {
        private List<string> menu = new List<string>() { "Back to search results", "Back to Customer Page", "Add to Cart"};
        private UserData loggedInUser;
        private Product? chosen;

        public DetailedProductPage(UserData loggedInUser, Product? chosen) {
            this.loggedInUser = loggedInUser;
            this.chosen = chosen;
        }
        public bool Run() {
            PrintHeader();
            GUI.PrintSelectedProduct(chosen);
            PrintMenu();
            PrintFooter();

            int choice = InputModule.SelectFromList(menu);

            switch (choice)
            {
                case 1:
                    return false;
                case 2:// back to customer page
                    return true;
                case 3: //add to cart
                    loggedInUser.ShoppingCart.Products.Add(chosen);
                    Console.WriteLine($"1 {chosen} has been added to your shopping cart");
                    Console.ReadKey(true);
                    break;
            }

            return false;
        }

        public void PrintHeader() {
            
        }

        public void PrintMenu() {
            
        }
        public void PrintFooter() {
            
        }

    }
}