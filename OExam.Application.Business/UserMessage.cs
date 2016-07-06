using Component.Tools;
using OExam.App.ViewModels;
using OExam.Core.Business;
using OExam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.App.Business
{
    public class UserMessage:UserMessageService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="luser"></param>
        /// <returns></returns>
        public OperationResult<UserData> Login(LoginUser luser)
        {
            OperationResult<UserData> result = new OperationResult<UserData>(EOperationType.Unsuccess);
            OperationResult<User> oResult = new OperationResult<User>(EOperationType.Success);
            oResult = base.CheckUser(luser.LoginName, luser.LoginPassword,luser.LoginType);
            if (oResult.ResultType == EOperationType.Success)
            {
                User user = oResult.AppentData;
                UserData udata = new UserData();
                udata.UserID = user.ID;
                udata.UserName = user.RealName;
                if(user.Department != null)
                    udata.DepartmentName = user.Department.Name;
                if(user.Role != null)
                    udata.RoleName = user.Role.Name;
                
                //得到权限菜单
                udata.TitlePages = new List<WindowPage>();
                udata.WindowGroups = GetRoleMenus(user,udata.TitlePages);

                result.AppentData = udata;
                result.ResultType = EOperationType.Success;
            }
            else
            {
                result.ResultType = oResult.ResultType;
                result.Message = oResult.Message;
            }
            return result;
        }

        private List<WindowGroup> GetRoleMenus(User user, List<WindowPage> titles)
        {
            List<MenuPage> lpages = new List<MenuPage>();
            GetMenuPages(user.Role, lpages);

            List<WindowGroup> lgroups = new List<WindowGroup>();

            foreach (var g in MenuGroups)
            {
                if (g.MenuPages != null)
                {
                    WindowGroup mg = new WindowGroup();
                    mg.Display = g.DisplayName;
                    mg.Pages = new List<WindowPage>();
                    foreach (var p in g.MenuPages.OrderBy(o=>o.OrderNum))
                    {
                        for(int i =0;i<lpages.Count;i++)
                        {
                            if(string.Compare(lpages[i].DisplayName,p.DisplayName)==0)
                            {
                                lpages.RemoveAt(i);

                                WindowPage page = new WindowPage();
                                page.Display = p.DisplayName;
                                page.PageUri = p.PageUri;
                                mg.Pages.Add(page);

                                i--;
                            }
                        }
                        
                    }
                    if (mg.Pages.Count > 0)
                        lgroups.Add(mg);
                }
            }

            if (lpages.Count > 0)
            {
                foreach(var p in lpages)
                {
                    WindowPage page = new WindowPage();
                    page.Display = p.DisplayName;
                    page.PageUri = p.PageUri;

                    titles.Add(page);
                }
                
                lpages.Clear();
            }
            return lgroups;
        }

    }
}
