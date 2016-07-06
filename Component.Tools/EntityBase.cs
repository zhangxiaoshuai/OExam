using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Tools
{
    public abstract class EntityBase<TKey>
    {
        protected EntityBase()
        {
            CreateTime = DateTime.Now;
            Deleted = false;
        }

        public TKey ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Deleted { get; set; }
        public byte[] TimeStamp { get; set; }
    }
}
