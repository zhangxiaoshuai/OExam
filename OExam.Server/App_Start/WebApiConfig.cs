using OExam.Server.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OExam.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //注入 response handler
            //config.MessageHandlers.Add(new HttpResponseHandlerHelp());
            //添加权限认证过滤器
            config.Filters.Add(new AuthFilterAttribute());

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            
        }
    }
}
