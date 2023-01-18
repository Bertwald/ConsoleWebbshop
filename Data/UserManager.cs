using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data
{
    internal class UserManager
    {
        public static List<Person> Users { get; set; }

        public static List<Person> GetAllUsers()
        {
            if (Users == null || !Users.Any())
            {
                Users = new List<Person>() {
                    new Person() { BirthDate = "19780215-1234", FirstName  = "John", LastName = "Andersson", Address = "Storgatan 12", City = "Stockholm", PostalCode = 11451, Country = "Sweden",  Telephone = "0705482135", MailAdress = "john.andersson@gmail.com"  },
                    new Person() { BirthDate = "19650306-1342", FirstName  = "Maria", LastName = "Claesson", Address = "Ringvägen 7", City = "Nyköping", PostalCode = 61165, Country = "Sweden",  Telephone = "0765872154", MailAdress = "maria.claesson@gmail.com"  },
                    new Person() { BirthDate = "19831112-1367", FirstName = "Ida", LastName = "Spjut", Address = "Aspvägen 3", City = "Umeå", PostalCode = 90354, Country = "Sweden", Telephone = "0706582347", MailAdress = "idaspjut@hotmail.com"},
                    new Person() { BirthDate = "19921220-1538", FirstName = "Felix", LastName = "Persson", Address = "Bergagatan 5", City = "Norrköping", PostalCode = 60375, Country = "Sweden", Telephone = "0739852417", MailAdress = "felixp@gmail.com"},
                    new Person() { BirthDate = "19750112-1284", FirstName = "Arthur", LastName = "King of britain", Address = "Big Castle 1", City = "London", PostalCode = 72594, Country = "Great Britain", Telephone = "0705419863", MailAdress = "arthur@king.com"},
                };
            }

            return Users;
        }
    }
}
