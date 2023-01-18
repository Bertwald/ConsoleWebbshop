using TestWebbshopCodeFirst.Pages;

namespace TestWebbshopCodeFirst {
    internal class Program {
        static void Main(string[] args) {

            Console.CursorVisible = false;
            new Start().Run();

            //InsertTestData.InsertData();
            //InsertTestData.SetRandomChosen();
            //InsertTestData.CreateUserAccounts();
        }
    }
}