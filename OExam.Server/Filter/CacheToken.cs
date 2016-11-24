using OExam.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OExam.Server.Filter
{
    public class CacheToken
    {
        private const string MCacheName = "passport.token";
        
        class CacheUser
        {
            public string username { get; set; }
            public string rolename { get; set; }
            public DateTime timeout { get; set; }
        }
        private static void Init()
        {
            if (HttpRuntime.Cache[MCacheName] == null)
            {
                Dictionary<string, CacheUser> dic = new Dictionary<string, CacheUser>();
                HttpRuntime.Cache.Insert(MCacheName, dic, null, DateTime.MaxValue, TimeSpan.FromDays(7));
            }
        }
        private static CacheUser GetTokenCacheUser(string token)
        {

            CacheUser cuser = null;
            Dictionary<string, CacheUser> dictoken = GetCacheToken();

            if (dictoken.ContainsKey(token))
            {
                cuser = dictoken[token];
            }

            return cuser;
        }

        private static Dictionary<string, CacheUser> GetCacheToken()
        {
            Init();
            return HttpRuntime.Cache[MCacheName] as Dictionary<string, CacheUser>;
        }

        public static void TokenTimeUpdate(string token, DateTime time)
        {
            var cuser = GetTokenCacheUser(token);
            if(cuser != null)
                cuser.timeout = time;
        }

        public static void TokenInsert(string token, string uname, string rolename,DateTime time)
        {
            if(TokenExist(token))
            {
                TokenTimeUpdate(token, time);
            }
            else
            {

                Dictionary<string, CacheUser> dictoken = GetCacheToken();
                CacheUser cuser = new CacheUser();
                cuser.username = uname;
                cuser.rolename = rolename;
                cuser.timeout = time;
                dictoken.Add(token, cuser);
            }
        }
        public static bool TokenRemove(string token)
        {
            bool removed = false;

            Dictionary<string, CacheUser> dictoken = GetCacheToken();
            if (dictoken.ContainsKey(token))
            {
                dictoken.Remove(token);
            }

            return removed;
        }
        public static bool TokenExist(string token)
        {
            bool exist = false;

            CacheUser cuser = null;
            Dictionary<string, CacheUser> dictoken = GetCacheToken();

            if (dictoken.ContainsKey(token))
            {

                cuser = dictoken[token];
                if (cuser == null)
                {
                    dictoken.Remove(token);
                }
                else
                {
                    if (DateTime.Now < cuser.timeout)
                        exist = true;
                    else
                        dictoken.Remove(token);
                }
            }

            return exist;
        }
    }
}