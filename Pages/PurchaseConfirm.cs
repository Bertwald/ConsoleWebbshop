using TestWebbshopCodeFirst.Interfaces;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class PurchaseConfirm : IPage
    {
        private UserData loggedInUser;

        public PurchaseConfirm(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }

        public void PrintFooter()
        {
            //Console.SetCursorPosition(30, 20);
            //Console.WriteLine($"Your Order Identification number is: ");
        }

        public void PrintHeader()
        {
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("Thank you for your purchase, we congratulate you for your excellent choice!");
            Console.SetCursorPosition(30, 6);
            Console.WriteLine("Your receipt will be sent to your e-mail address: " + loggedInUser.Person.MailAdress);
            PrintReturnPolicy();
            PrintNi();
        }

        public void PrintMenu() { }
        public bool Run()
        {
            if (!loggedInUser.Person.Customers.Any())
            {
                GUI.ClearWindow();
                Console.SetCursorPosition(40, 5);
                Console.WriteLine("F@ckµp with account, contact Admin!");
                Console.SetCursorPosition(40, 6);
                GUI.Delay();
                return true;
            }

            Order newOrder = CreateOrder();
            int affectedRows;
            using (var db = new WebshopDbContext())
            {
                db.Attach(newOrder);
                UpdateProductStock(newOrder);
                db.Add(newOrder);
                affectedRows = db.SaveChanges();
                db.Dispose();
            }
            if (affectedRows < 1)
            {
                Console.WriteLine("Fuckup in database");
                GUI.Delay();
                return true;
            }
            else
            {
                GUI.ClearWindow();
                PrintHeader();
                PrintMenu();
                PrintFooter();
                int orderId = newOrder.Id;
                DateTime orderDate = (DateTime)newOrder.OrderDate;
                Console.SetCursorPosition(30, 10);
                Console.WriteLine("Your order identification number is: " + orderId);
                Console.SetCursorPosition(30, 12);
                Console.WriteLine("Orderdate: " + orderDate.ToString("R"));
                Console.SetCursorPosition(30, 26);
                GUI.Delay();
                return true;
            }           
        }

        private static void UpdateProductStock(Order newOrder) {
            foreach(OrderDetail od in newOrder.OrderDetails) {
                od.Product.UnitsInStock -= od.Quantity;
            }
        }

        private Order CreateOrder()
        {
            Order newOrder = new Order()
            {
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
            foreach (var detail in loggedInUser.ShoppingCart.OrderDetails)
            {
                newOrder.OrderDetails.Add(new OrderDetail()
                {
                    Order = detail.Order,
                    Product = detail.Product,
                    UnitPrice = detail.UnitPrice,
                    Quantity = detail.Quantity,
                    OrderId = detail.OrderId,
                    ProductId = detail.ProductId,
                });
            }
            return newOrder;
        }

        private static void PrintReturnPolicy()
        {
            DateTime orderDate = DateTime.Now;
            Console.SetCursorPosition(30, 20);
            Console.WriteLine("RETURN POLICY");
            Console.SetCursorPosition(30, 21);
            Console.WriteLine("You have the right to regret your purchase to " + orderDate.AddDays(7).ToString("R"));
            Console.SetCursorPosition(30, 22);
            Console.WriteLine("Please contact us to let us know that your order will be returned.");
        }

        private static void PrintNi()
        {
            Console.SetCursorPosition(30, 24);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Now go out in the woods and get a shrubbery for the head Knight of Ni!");
            Console.ResetColor();
            
        }
    }
}