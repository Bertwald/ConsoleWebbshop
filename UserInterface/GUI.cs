using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.UserInterface
{
    internal class GUI
    {
        //printmetoder
        internal static void PrintHeader(List<string> headerText)
        {
            foreach (string text in headerText)
            {
                Console.WriteLine($"Id: {slot.Id}\t{slot.CityName}           \t\t{slot.HouseName}           \t\t{(slot.ElectricOutlet ? "Elutttag" : "Ej Eluttag")}");
            }
        }
    }
}
