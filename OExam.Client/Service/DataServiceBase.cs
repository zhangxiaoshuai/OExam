using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http.Formatting;
using OExam.Client.Utils;
using Component.Tools;
using OExam.App.ViewModels;

namespace OExam.Client.Service
{
    abstract class DataServiceBase
    {
        public abstract void GetData<T>(Action<T> callback);
        
        public DataServiceBase()
        {
            _serviceAddress = new Uri("http://localhost:29279");
        }
        private Uri _serviceAddress;
        protected Uri ServiceAddress
        {
            get
            {
                return _serviceAddress;
            }
        }
        private HttpClient CreateClient()
        {
            var c = new HttpClient();
            c.BaseAddress = ServiceAddress;
            //添加TOKEN
            //c.DefaultRequestHeaders.Add("token", "aaa");
            return c;
        }
        /// <summary>
        /// 获取数据，从WEBAPI上
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="webFunAddress">WEBAPI方法的地址</param>
        protected async void GetModel(string webFunAddress, Action<OperationResult<UserData>> callback)
        {
            using (var c = CreateClient())
            {
                try
                {
                    HttpResponseMessage response = await c.GetAsync(webFunAddress);
                    
                    OperationResult<UserData> t = await response.Content.ReadAsAsync<OperationResult<UserData>>();
                    callback(t);
                }
                catch (Exception ex)
                {
                    callback(new OperationResult<UserData>(EOperationType.Exception, "服务器异常" + ex.Message));
                }

                //WebClient client = new WebClient();
                
                /*
                string message = await .GetStringAsync(webFunAddress);
                var serializer = new JavaScriptSerializer();
                callback(serializer.Deserialize<T>(message));
                */
            }
        }
        protected async void AddModel<T>(string webFunAddress, T model)
        {
            using (var c = CreateClient())
            {
                HttpContent content = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
                HttpResponseMessage message = await c.PostAsync(webFunAddress, content);
                message.EnsureSuccessStatusCode();
            }
        }
        protected async void UpdateModel<T>(string webFunAddress, T model)
        {
            using (var c = CreateClient())
            {
                HttpResponseMessage message = await c.PutAsJsonAsync(webFunAddress, model);
            }
        }
        protected async void DeleteModel(string webFunAddress)
        {
            try
            {
                using (var c = CreateClient())
                {
                    HttpResponseMessage message = await c.DeleteAsync(webFunAddress);
                    message.EnsureSuccessStatusCode();
                }
            }catch(HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
