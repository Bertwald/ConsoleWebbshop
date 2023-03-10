using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebbshopCodeFirst.Logic {
    public enum OrderStatus {
        InShoppingCart,
        Recieved,
        Packing,
        Shipping,
        Completed
    }
    public enum PayingOption {
        Creditcard = 1,
        Swish,
        Klarna
    }
    public enum ShippingOption {
        Tortoise = 1,
        Hare,
        African_Swallow
    }
    public enum Privilege {
        Visitor,
        Customer,
        Admin
    }
}
