using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Models
{
    public class Role : EntityInt
    {
        public virtual ICollection<Role> SunRoles { get; set; }
        public int Level { get; set; }
        public bool OwnSunPower { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<MenuPage> MenuPages { get; set; }
    }
}
