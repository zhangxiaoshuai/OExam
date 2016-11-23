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
    public class AuthFilter:ActionFilterAttribute
    {
        public string[] Roles { get; set; }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
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
                    //actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                    //}
                }
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }

    /// <summary>
    /// 空过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AnonymousFilter:Attribute
    { }
}