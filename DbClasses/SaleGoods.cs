using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresShopApplication.DbClasses
{
    public partial class SaleGoods : IDbClass
    {
        [Key]
        public int Id_SaleGoods { get; set; }
        public int Id_Goods { get; set; }
        public int id_Sale { get; set; }
        public int Amount { get; set; }

        public virtual AutoModif? IdModifNavigation { get; set; }
        public virtual Sale? IdSaleNavigation { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }
    }
}
