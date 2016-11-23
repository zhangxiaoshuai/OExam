using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OExam.Server.Controllers
{
    public class BaseApiController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            //获取表头
            var header = controllerContext.Request.Headers;
            //controllerContext.Request.Headers.Clear();
            //controllerContext.Request.Headers.Remove("token");
        }
    }
}
