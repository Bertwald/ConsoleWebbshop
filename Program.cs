using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TestWebbshopCodeFirst.Logic;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.Pages;

namespace TestWebbshopCodeFirst {
    internal class Program {
        static void Main(string[] args) {
            //AdminPage adminPage = new AdminPage(new User() { BirthDate = "19810112-1284", FirstName = "Christina", LastName = "Holm", Address = "Banjovägen 5", City = "Västerås", PostalCode = 72594, Country = "Sweden", Telephone = "0705419863", MailAdress = "c.holm@hotmail.com" });
            //adminPage.PrintHeader();
            //adminPage.PrintMenu();

            //using (OurDbContext db = new OurDbContext()) {
            //    var users = db.Users.Include("UserAccounts").Include("Employees").Include("Customers");
            //    foreach (var user in users) {
            //        Console.WriteLine(user.UserAccounts.Count + "  Es:" + user.Employees.Count + "  Cs" + user.Customers.Count);
            //    }
            //}

            InsertTestData.InsertData();
            InsertTestData.SetRandomChosen();
            InsertTestData.CreateUserAccounts();
        }
    }
}