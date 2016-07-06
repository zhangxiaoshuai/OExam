using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OExam.Core.Data
{
    class ContextFactory
    {
        private const string HttpContextKey = "efcontextkey";
        public static DbContext GetContext()
        {
            Database.SetInitializer(new InitData());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OExamDbContext>());
            //将DBCONTEXT放入HTTPCONTEXT中，单个用户单次请求生成共用一个DBCONTEXT
            OExamDbContext odb;
            if (HttpContext.Current == null)
            {
                odb = new OExamDbContext();
                //odb = null;
            }
            else
            {
                if (!HttpContext.Current.Items.Contains(HttpContextKey))
                {
                    HttpContext.Current.Items.Add(HttpContextKey, new OExamDbContext());
                }

                odb = (OExamDbContext)HttpContext.Current.Items[HttpContextKey];
            }

            return odb;
        }
    }
}
