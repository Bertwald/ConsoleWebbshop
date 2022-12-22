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
        public static List<User> Users { get; set; }

        public static List<User> GetAllUsers()
        {
            if (Users == null || !Users.Any())
            {
                Users = new List<User>() {
                    new User() { BirthDate = "19780215-1234", FirstName  = "John", LastName = "Andersson", Address = "Storgatan 12", City = "Stockholm", PostalCode = 11451, Country = "Sweden",  Telephone = "0705482135", MailAdress = "john.andersson@gmail.com"  },
                    new User() { BirthDate = "19650306-1342", FirstName  = "Maria", LastName = "Claesson", Address = "Ringvägen 7", City = "Nyköping", PostalCode = 61165, Country = "Sweden",  Telephone = "0765872154", MailAdress = "maria.claesson@gmail.com"  },
                    new User() { BirthDate = "19831112-1367", FirstName = "Ida", LastName = "Spjut", Address = "Aspvägen 3", City = "Umeå", PostalCode = 90354, Country = "Sweden", Telephone = "0706582347", MailAdress = "idaspjut@hotmail.com"},
                    new User() { BirthDate = "19921220-1538", FirstName = "Felix", LastName = "Persson", Address = "Bergagatan 5", City = "Norrköping", PostalCode = 60375, Country = "Sweden", Telephone = "0739852417", MailAdress = "felixp@gmail.com"},
                    new User() { BirthDate = "19810112-1284", FirstName = "Christina", LastName = "Holm", Address = "Banjovägen 5", City = "Västerås", PostalCode = 72594, Country = "Sweden", Telephone = "0705419863", MailAdress = "c.holm@hotmail.com"},
                };
            }

            return Users;
        }
    }
}
