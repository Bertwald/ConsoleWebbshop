using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TestWebbshopCodeFirst.Logic;

namespace TestWebbshopCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InsertTestData.InsertData();
        }
    }
}