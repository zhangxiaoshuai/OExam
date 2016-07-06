using OExam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Data
{
    class InitData : System.Data.Entity.DropCreateDatabaseIfModelChanges<OExamDbContext>
    {
        protected override void Seed(OExamDbContext context)
        {
            //base.Seed(context);
            Department pment = new Department();
            pment.Name = "教务部";
            Role role = new Role();
            role.Name = "老师";
            role.Level = 1;
            role.OwnSunPower = true;
            role.Department = pment;

            Role role2 = new Role();
            role2.Name = "学生";
            role2.Level = 2;
            role2.OwnSunPower = true;
            role2.Department = pment;

            role.SunRoles = new List<Role>();
            role.SunRoles.Add(role2);

            Teacher user = new Teacher();
            user.Name = "teacher";
            user.RealName = "张老师";
            user.Role = role;
            user.Password = "123";

            role.MenuPages = new List<MenuPage>();

            MenuGroup mgroup = new MenuGroup();
            mgroup.DisplayName = "基础信息维护";
            mgroup.OrderNum = 0;
            mgroup.MenuPages = new List<MenuPage>();

            MenuPage mpage = new MenuPage();
            mpage.DisplayName = "学院维护";
            mpage.PageUri = "/Pages/BasicInfo/CollegeModifyPage.xaml";
            mpage.OrderNum = 0;
            role.MenuPages.Add(mpage);
            mgroup.MenuPages.Add(mpage);

            mpage = new MenuPage();
            mpage.DisplayName = "专业维护";
            mpage.PageUri = "/Pages/BasicInfo/MajorModifyPage.xaml";
            mpage.OrderNum = 1;
            role.MenuPages.Add(mpage);
            mgroup.MenuPages.Add(mpage);

            mpage = new MenuPage();
            mpage.DisplayName = "班级维护";
            mpage.PageUri = "/Pages/BasicInfo/ClassModifyPage.xaml";
            mpage.OrderNum = 2;
            role.MenuPages.Add(mpage);
            mgroup.MenuPages.Add(mpage);

            mpage = new MenuPage();
            mpage.DisplayName = "学生维护";
            mpage.PageUri = "/Pages/BasicInfo/StudentModifyPage.xaml";
            mpage.OrderNum = 3;
            role.MenuPages.Add(mpage);
            mgroup.MenuPages.Add(mpage);

            mpage = new MenuPage();
            mpage.DisplayName = "教师维护";
            mpage.PageUri = "/Pages/BasicInfo/TeacherModifyPage.xaml";
            mpage.OrderNum = 4;
            role.MenuPages.Add(mpage);
            mgroup.MenuPages.Add(mpage);

            context.Set<MenuGroup>().Add(mgroup);
            context.Set<User>().Add(user);

            context.SaveChanges();
        }
    }
}
