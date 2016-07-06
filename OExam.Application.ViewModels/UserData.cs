using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.App.ViewModels
{
    public class UserData
    {
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public List<WindowGroup> WindowGroups { get; set; }
        public List<WindowPage> TitlePages { get; set; }
    }

    public class WindowPage
    {
        public string Display { get; set; }
        public string PageUri { get; set; }
    }
    public class WindowGroup
    {
        public string Display { get; set; }
        public List<WindowPage> Pages;
    }
}
