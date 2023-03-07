using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication
{
    internal class CartCollectionWheel
    {
        private static List<InnerWheel> CartItems = new List<InnerWheel>();
      

        public static List<InnerWheel> GetItems()
        {
            return CartItems;
        }
        public static void AddItem(InnerWheel item)
        {
            CartItems.Add(item);
        }
        public static void Clear()
        {
            CartItems.Clear();
        }
       
    }
}
