using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Models;

namespace TestWebbshopCodeFirst.Data
{
    internal class EmployeeManager
    {
        public static List<Employee> Employees { get; set; }

        public static List<Employee> GetAllEmployees()
        {
            if (Employees == null || !Employees.Any())
            {
                Employees = new List<Employee>() {
                    new Employee() { UserId = 5, HireDate = new DateTime(2021,10,12) }
                };
            }

            return Employees;
        }

    }
}
