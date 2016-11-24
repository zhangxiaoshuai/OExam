using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OExam.Server.Filter
{
    /// <summary>
    /// 权限认证过滤器
    /// </summary>
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        public string[] Roles { get; set; }

        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    //去掉过滤器验证
        //    var actionFilter = actionContext.ActionDescriptor.GetCustomAttributes<AnonymousAttribute>(false);
        //    var controllerFilter = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AnonymousAttribute>(false);
        //    if (actionFilter.Count == 1 || controllerFilter.Count == 1)
        //    {
        //        return;
        //    }
        //    base.OnActionExecuting(actionContext);
        //}
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //去掉过滤器验证
            var actionFilter = actionContext.ActionDescriptor.GetCustomAttributes<AnonymousAttribute>(false);
            var controllerFilter = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AnonymousAttribute>(false);
            if (actionFilter.Count == 1 ||controllerFilter.Count == 1)
            {
                //返回空的TASK以免在继续调用OnActionExecuting方法，只需在此做一次判定是否为空过滤器的 控制器或ACTION
                return Task.Factory.StartNew(()=> { });
            }


            string tokenheader = "token";
            var header = actionContext.Request.Headers;
            if (header.Contains(tokenheader))
            {
                var tokenvals = header.GetValues(tokenheader).ToList();
                
                if(tokenvals.Count()>0)
                {
                    string token = tokenvals[0];
                    //添加TOKEN验证
                    //if(!CacheToken.TokenExist(token))
                    //{
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                    //}
                }
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }

    /// <summary>
    /// 空过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AnonymousAttribute : Attribute
    { }
}