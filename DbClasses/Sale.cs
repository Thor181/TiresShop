using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiresShopApplication.DbClasses;

namespace TiresShopApplication
{
    public partial class Sale : IDbClass
    {
        public Sale()
        {
           
            SaleGood = new HashSet<SaleGoods>();
        }
        [Key]
        public int IdSale { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateOrder { get; set; }

        //public virtual AutoTire? IdSaleNavigation { get; set; }
        
        public virtual ICollection<SaleGoods> SaleGood { get; set; }
    }
}
