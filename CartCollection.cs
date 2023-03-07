using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TiresShopApplication
{
    internal class CartCollection
    {
        private static List<InnerTiresWheelClass> CartItems = new List<InnerTiresWheelClass>();
        private static List<InnerWheel> WheelsItems = new List<InnerWheel>();
        private static List<InnerShopPlus> ShopPlusItems = new List<InnerShopPlus>();

        public static List<InnerShopPlus> GetItemsShop()
        {
            return ShopPlusItems;
        }
        public static List<InnerWheel> GetItems1()
        {
            return WheelsItems;
        }

        public static List<InnerTiresWheelClass> GetItems()
        {
            return CartItems;
        }

        public static void AddItem(InnerTiresWheelClass item)
        {
            using (var db = new TiresDbContext())
            {
                if (item.Amount == 0)
                {
                    MessageBox.Show("Нету в наличии!");
                }
                else
                {
                    IEnumerable<AutoTire> customers = db.AutoTires
                        .Where(c => c.Id == item.Id)
                        .Select(c => c.Id)
                        .AsEnumerable()
                        .Select(id => new AutoTire
                        {
                            Id = id,
                            Amount = item.Amount - 4,
                        });

                    foreach (AutoTire customer in customers)
                    {
                        // Указать, что запись изменилась
                        db.AutoTires.Attach(customer);
                        db.Entry(customer)
                            .Property(c => c.Amount).IsModified = true;
                    }

                    // Сохранить изменения
                    db.SaveChanges();
                    CartItems.Add(item);
                }
            }
        }
        public static void AddItem1(InnerTiresWheelClass item)
        {

            using (var db = new TiresDbContext())
            {

                if (item.Amount == 0)
                {
                    MessageBox.Show("Нету в наличии!");

                }
                else
                {
                    CartItems.Add(item);
                }
            }
        }

        public static void Clear()
        {
            foreach (var item in GetItems())
            {
                TiresDbContext context = new TiresDbContext();
                
                IEnumerable<AutoTire> customers = context.AutoTires

                    .Where(c => c.Id == item.Id)
                    .Select(c => c.Id)
                    .AsEnumerable()

                    .Select(id => new AutoTire
                    {
                        Id = id,
                        Amount = item.Amount,
                    });

                foreach (AutoTire customer in customers)
                {
                    // Указать, что запись изменилась
                    context.AutoTires.Attach(customer);
                    context.Entry(customer)
                        .Property(c => c.Amount).IsModified = true;
                }

                // Сохранить изменения
                context.SaveChanges();
            }
            CartItems.Clear();


        }
        public static void ClearBye()
        {
            CartItems.Clear();
        }
        public static decimal GetSum()
        {
            return CartItems.Select(x => x.Price).Sum();
        }
        public static void RemoveItem(int id)
        {

            CartItems.Remove(CartItems.First(x => x.Id == id));
        }
    }
}
