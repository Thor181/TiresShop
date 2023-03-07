using System;
using System.Collections.Generic;
using TiresShopApplication.DbClasses;

namespace TiresShopApplication
{
    public partial class AutoModif : IDbClass
    {
        public AutoModif()
        {
            AutoTires = new HashSet<AutoTire>();
            AutoWheels = new HashSet<AutoWheel>();
            SaleGood = new HashSet<SaleGoods>();
        }

        public int Id { get; set; }
        public int? IdModels { get; set; }
        public int? Year { get; set; }
        public string? Name { get; set; }
        public string? Diameter { get; set; }

        public virtual AutoModel? IdModelsNavigation { get; set; }
        public virtual ICollection<AutoTire> AutoTires { get; set; }
        public virtual ICollection<AutoWheel> AutoWheels { get; set; }
        public virtual ICollection<SaleGoods> SaleGood { get; set; }

    }
}
