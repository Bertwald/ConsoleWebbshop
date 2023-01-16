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

        public void PrintMenu() {
            //throw new NotImplementedException();
        }
        public bool Run() {
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
            bool alt = false;
            string[] adress = new string[5];
            alt = loggedInUser.ShoppingCart.GetAlternativeAddress().Any();
            if (alt) {
                adress = loggedInUser.ShoppingCart.GetAlternativeAddress().Split(',');
            }

            Order newOrder = new Order() {
                //Custumer = loggedInUser.Person.Customers.First(),
                //OrderDate = DateTime.Now,
                //PayingOption = loggedInUser.ShoppingCart.PayingOption,
                //TotalPrice = loggedInUser.GetTotalPrice(),
                //ShippingOption = loggedInUser.ShoppingCart.ShippingOption,
                //ShippingAdress = alt ? adress[0] : loggedInUser.Person.Address,
                //ShippingCity = alt ? adress[1] : loggedInUser.Person.City,
                //ShippingPostalcode = alt ? int.Parse(adress[2].Trim()) : loggedInUser.Person.PostalCode,
                //ShippingCountry = alt ? adress[3] : loggedInUser.Person.Country,
                //Orderstatus = OrderStatus.Recieved
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