using System;
using System.Collections.Generic;

namespace EfCore3Test.Db.EF
{
    public partial class NodesData
    {
        public int DataId { get; set; }
        public int NodeId { get; set; }
        public DateTime Created { get; set; }

        public virtual Nodes Node { get; set; }
    }
}
