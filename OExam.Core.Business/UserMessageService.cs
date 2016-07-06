using Component.Tools;
using OExam.Core.Data;
using OExam.Core.Data.Repository;
using OExam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Business
{
    public abstract class UserMessageService
    {
        
        private static List<MenuGroup> MGroups;
        protected UserRepository _userR = new UserRepository();
        protected StudentRepository _studentR = new StudentRepository();
        protected TeacherRepository _teacherR = new TeacherRepository();
        protected RoleRepository _roleR = new RoleRepository();
        private MenuGroupRepository _mgroupR = new MenuGroupRepository();

        protected List<MenuGroup> MenuGroups
        {
            get
            {
                if (MGroups == null)
                    MGroups = _mgroupR.Entities.OrderBy(o=>o.OrderNum).ToList();
                return MGroups;
            }
        }
        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="usertype">用户类型：0学生，1老师，2管理员</param>
        /// <returns></returns>
        protected OperationResult<User> CheckUser(string username,string password,int usertype)
        {
            OperationResult<User> oResult = new OperationResult<User>(EOperationType.Success);

            string usertypename = "管理员";
            User user = null;
            switch (usertype)
            {
                case 0:
                    user = _studentR.Entities.SingleOrDefault(u => u.Name == username);
                    usertypename = "学生";
                    break;
                case 1:
                    user = _teacherR.Entities.SingleOrDefault(u => u.Name == username);
                    usertypename = "老师";
                    break;
                case 2:
                    user = _userR.Entities.SingleOrDefault(u => u.Name == username);
                    break;
            }

            //User user = _userR.Entities.SingleOrDefault(u => u.Name == username);
            if (user == null)
            {
                oResult.ResultType = EOperationType.None;
                oResult.Message = "此" + usertypename + "不存在";
            }
            else if (string.Compare(user.Password , password)==0)
            {
                oResult.AppentData = user;
            }
            else
            {
                oResult.ResultType = EOperationType.Error;
                oResult.Message = "密码错误";
            }

            return oResult;

        }
        /// <summary>
        /// 得到权限的菜单页集合
        /// </summary>
        /// <param name="role"></param>
        /// <param name="lpages"></param>
        protected void GetMenuPages(Role role, List<MenuPage> lpages)
        {
            if (lpages == null)
                return;

            foreach (var p in role.MenuPages.OrderBy(o=>o.OrderNum))
            {
                if (lpages.Contains(p))
                    continue;

                lpages.Add(p);
            }

            if (role.OwnSunPower && role.SunRoles != null)
            {
                foreach (var r in role.SunRoles)
                {
                    GetMenuPages(r, lpages);
                }
            }
        }

    }
}
