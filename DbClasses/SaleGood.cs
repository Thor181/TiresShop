using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication.DbClasses
{
    public class SaleGood
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateOrder { get; set; }
        public int IdAutoTire { get; set; }
        public int Amount { get; set; }
        public virtual AutoTire? IdAutoTireNavigation { get; set; }
    }
}
