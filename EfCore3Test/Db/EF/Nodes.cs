using System;
using System.Collections.Generic;

namespace EfCore3Test.Db.EF
{
    public partial class Nodes
    {
        public Nodes()
        {
            NodesData = new HashSet<NodesData>();
        }

        public int NodeId { get; set; }
        public int Nr { get; set; }

        public virtual ICollection<NodesData> NodesData { get; set; }
    }
}
