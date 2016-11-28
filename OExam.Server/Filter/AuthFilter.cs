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

            var header = actionContext.Request.Headers;
            //添加TOKEN验证
            if (header.Contains(TokenCache.TOKENNAME))
            {
                var tokenvals = header.GetValues(TokenCache.TOKENNAME).ToList();
                
                if(tokenvals.Count()>0)
                {
                    string[] tokenmsg = tokenvals[0].Split(TokenCache.TOKENSPLIT);
                    if (tokenmsg.Length > 1)
                    {
                        string username = tokenmsg[0];
                        string token = tokenmsg[1];
                        
                        if (TokenCache.CheckTokenExist(username, token))
                        {
                            return base.OnActionExecutingAsync(actionContext, cancellationToken);
                        }
                    }
                    
                }
            }
            //验证未通过，返回请求资源不可用
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Gone);
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