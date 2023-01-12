using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Pages
{
    internal class RegistrationPage
    {
        internal bool Run()
        {
            Console.Write("Please enter your birthdate: ");
            var birthDate = InputModule.GetString();
            Console.Write("Please enter your firstname: ");
            var firstName = InputModule.GetString();
            Console.Write("Please enter your lastname: ");
            var lastName = InputModule.GetString();
            Console.Write("Please enter your address: ");
            var address = InputModule.GetString();
            Console.Write("Please enter your City: ");
            var city = InputModule.GetString();
            Console.Write("Please enter your postal code: ");
            var postalCode = InputModule.GetInt();
            Console.Write("Please enter your country: ");
            var country = InputModule.GetString();
            Console.Write("Please enter your phonenumber: ");
            var phoneNr = InputModule.GetString();
            Console.Write("Please enter your email: ");
            var email = InputModule.GetString();
            Console.Write("Please enter your creditcard nr in XXXX XXXX XXXX XXXX");
            var creditCardNr = InputModule.GetString();
            //Validera siffrorna i input module
            Console.Write("Please enter a username: ");
            var userName = InputModule.GetString();
            Console.Write("Please enter a password: ");
            var password = InputModule.GetString();

            using (var db = new OurDbContext())
            {
                var newPerson = new Person
                {
                    BirthDate = birthDate,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    City = city,
                    PostalCode = postalCode,
                    Country = country,
                    Telephone = phoneNr,
                    MailAdress = email
                };
                var newCustomer = new Customer
                {
                    Person = newPerson,
                    ShippingAdresses = newPerson.Address,
                    CreditCardNumber = creditCardNr                   
                };
                var newAccount = new Account
                {
                    Username = userName,
                    Password = password,
                    Privilege = Logic.Privilege.Customer,
                    User = newPerson
                };
                db.Add(newPerson);
                db.Add(newCustomer);
                db.Add(newAccount);
                db.SaveChanges();
            }
            return false;
        }
    }
}
