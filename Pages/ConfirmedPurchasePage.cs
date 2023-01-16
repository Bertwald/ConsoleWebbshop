using TestWebbshopCodeFirst.Logic;

namespace TestWebbshopCodeFirst.Pages
{
    internal class ConfirmedPurchasePage : IPage
    {
        private UserData loggedInUser;

        public ConfirmedPurchasePage(UserData loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }

        public void PrintFooter()
        {
            throw new NotImplementedException();
        }

        public void PrintHeader()
        {
            throw new NotImplementedException();
        }

        public void PrintMenu()
        {
            throw new NotImplementedException();
        }

        public bool Run()
        {

        }
    }
}