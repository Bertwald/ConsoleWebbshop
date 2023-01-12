﻿using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;

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
            PrintMenu();
            PrintFooter();

            return false;
        }

        public void PrintHeader() {
            throw new NotImplementedException();
        }

        public void PrintMenu() {
            throw new NotImplementedException();
        }
        public void PrintFooter() {
            throw new NotImplementedException();
        }

    }
}