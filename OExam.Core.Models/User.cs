using Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Models
{
    public class User: EntityInt
    {
        public string Password { get; set; }

        public string RealName { get; set; }
        public virtual Role Role { get; set; }
        public virtual Department Department { get; set; }
    }
}
