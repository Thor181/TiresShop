using System;
using System.Collections.Generic;

namespace TiresShopApplication
{
    public partial class AutoMark : IDbClass
    {
        public AutoMark()
        {
            AutoModels = new HashSet<AutoModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AutoModel> AutoModels { get; set; }
    }
}
