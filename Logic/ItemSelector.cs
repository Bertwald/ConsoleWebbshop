using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.UserInterface;

namespace TestWebbshopCodeFirst.Logic {
    internal class ItemSelector<T> where T : class {
        internal static T? GetItemFromList(List<T> items) {
            if (!items.Any()) {
                return null;
            }

            List<string> itemStrings = new List<string>();
            foreach (T item in items) {
                itemStrings.Add(item.ToString()!);
            }
            int itemIndex = InputModule.SelectFromList(itemStrings);
            return items[itemIndex];
        }
    }
}
