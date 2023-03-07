using System;
using System.Collections.Generic;

namespace TiresShopApplication
{
    public partial class AutoModel : IDbClass
    {
        public AutoModel()
        {
            AutoModifs = new HashSet<AutoModif>();
        }

        public int Id { get; set; }
        public int? IdMark { get; set; }
        public string Name { get; set; } = null!;

        public virtual AutoMark? IdMarkNavigation { get; set; }
        public virtual ICollection<AutoModif> AutoModifs { get; set; }
    }
}
