using Component.Tools;
using OExam.App.ViewModels;
using OExam.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Client.Service
{
    class UserDataService : DataServiceBase
    {
        public override void GetData<T>(Action<T> callback)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过登录用户得到菜单数据
        /// </summary>
        /// <typeparam name="List"></typeparam>
        /// <param name="luser"></param>
        /// <param name="callback"></param>
        public void CheckAndGetUser(LoginUser luser,Action<OperationResult<UserData>> callback)
        {
            string requestUri = "/api/UserManager?username=" + luser.LoginName + "&password=" + luser.LoginPassword + "&roletype=" + luser.LoginType;
            //string requestUri = "/api/UserManager?id=1323" ;
            base.GetModel(requestUri, callback);
        }
    }
}
