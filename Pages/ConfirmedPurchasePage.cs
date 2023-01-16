using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages {
    internal class ConfirmedPurchasePage : IPage {
        private UserData loggedInUser;

        public ConfirmedPurchasePage(UserData loggedInUser) {
            this.loggedInUser = loggedInUser;
        }

        public void PrintFooter() {
            Console.SetCursorPosition(30, 20);
            Console.WriteLine($"Your Order Identification number");
        }

        public void PrintHeader() {
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("Thank you for your purchase, we congratulate you for your excellent choice!");
        }

        public void PrintMenu() {}
        public bool Run() {
            if (!loggedInUser.Person.Customers.Any()) {
                GUI.ClearWindow();
                Console.SetCursorPosition(40, 5);
                Console.WriteLine("F@ckµp with account, contact Admin!");
                Console.SetCursorPosition(40, 6);
                GUI.Delay();
                return true;
            }

            Order newOrder = CreateOrder();

            int affectedRows;
            using (var db = new OurDbContext())
            {
                db.Attach(newOrder);
                db.Add(newOrder);            
                affectedRows = db.SaveChanges();
                db.Dispose();
            }
            if (affectedRows < 1)
            {
                Console.WriteLine("Fuckup in database");
                GUI.Delay();
            }

            GUI.ClearWindow();
            PrintHeader();
            PrintMenu();
            PrintFooter();
            GUI.Delay();
            return true;
        }

        private Order CreateOrder() {
            Order newOrder = new Order() {
                Custumer = loggedInUser.Person.Customers.First(),
                OrderDate = DateTime.Now,
                PayingOption = loggedInUser.ShoppingCart.PayingOption,
                TotalPrice = loggedInUser.GetTotalPrice() + ItemSelector<Order>.GetShippingCost(loggedInUser.ShoppingCart.ShippingOption),
                ShippingOption = loggedInUser.ShoppingCart.ShippingOption,
                ShippingAdress = loggedInUser.ShoppingCart.ShippingAdress ?? loggedInUser.Person.Address,
                ShippingCity = loggedInUser.ShoppingCart.ShippingCity ?? loggedInUser.Person.City,
                ShippingPostalcode = loggedInUser.ShoppingCart.ShippingPostalcode ?? loggedInUser.Person.PostalCode,
                ShippingCountry = loggedInUser.ShoppingCart.ShippingCountry ?? loggedInUser.Person.Country,
                Orderstatus = OrderStatus.Recieved
            };
            foreach(var detail in loggedInUser.ShoppingCart.OrderDetails) {
                newOrder.OrderDetails.Add(new OrderDetail() {
                    Order = detail.Order,
                    Product = detail.Product,
                    UnitPrice = detail.UnitPrice,
                    Quantity = detail.Quantity,
                    OrderId = detail.OrderId,
                    ProductId= detail.ProductId,
                });
            }
            return newOrder;
        }
    }
}