
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TiresShopApplication
{
    internal class InnerSaleGood
    {
        public int IdSale { get; set; }
        public DateTime DataOrder { get; set; }
        public decimal Cost { get; set; }
        //public int IdGoods { get; set; }
        public string NameGoods { get; set; }
        public int Amount { get; set; }
        private int IdGood { get; set; }


        public InnerSaleGood(int idSale, DateTime dataOrder, decimal cost, int amount, int idGood)
        {
            IdSale = idSale;
            DataOrder = dataOrder;
            Cost = cost;
            NameGoods = GetNameAutoTire(idGood);
            Amount = amount;
            IdGood = idGood;
        }
        private string GetNameAutoTire(int idGood)
        {
            using (TiresDbContext db = new())
            {
                string name = db.AutoTires.Where(x => x.Id == idGood)
                    .Include(x => x.IdModifNavigation)
                    .ThenInclude(x => x!.IdModelsNavigation)
                    .ThenInclude(x => x!.IdMarkNavigation)
                    .Select(x => $"{x.IdModifNavigation!.IdModelsNavigation!.IdMarkNavigation!.Name} " +
                                 $"{x.IdModifNavigation.IdModelsNavigation.Name} " +
                                 $"{x.IdModifNavigation.Name} " +
                                 $"{x.IdModifNavigation!.Year}").First();
                return name;
            }
        }
    }
}
