using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Models
{
    public class MenuGroup:EntityInt
    {
        public string DisplayName { get; set; }
        public virtual ICollection<MenuPage> MenuPages { get; set; }
        public int OrderNum { get; set; }
    }
}
