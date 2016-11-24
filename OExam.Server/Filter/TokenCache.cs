using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace OExam.Server.Filter
{
    public class TokenCache
    {
        //令牌存入缓存中
        /// <summary>
        /// key   用户名
        /// value token
        /// </summary>
        private static Dictionary<string, TokenModel> MTokens = new Dictionary<string, TokenModel>();

        //默认无操作60分钟后登录状态无效
        private const int _limitOperateMinTime = 60;
        /// <summary>
        /// 令牌名称
        /// </summary>
        public static string TOKENNAME = "token";
        /// <summary>
        /// 令牌信息分隔符
        /// </summary>
        public static char TOKENSPLIT = '$';
        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="token">令牌字符串</param>
        /// <returns></returns>
        public static bool CheckTokenExist(string user,string token)
        {
            if(MTokens.ContainsKey(user))
            {
                TokenModel tmodel = MTokens[user];
                if(tmodel != null && string.Compare(tmodel.Tokenid, token)==0)
                {
                    if ((DateTime.Now - tmodel.LastOperateTime).TotalMinutes < _limitOperateMinTime)
                    {
                        tmodel.LastOperateTime = DateTime.Now;
                        return true;
                    }
                    
                }
            }

            return false;
        }
        /// <summary>
        /// 存储令牌，并返回令牌字符串
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string SaveToken(string user)
        {
            TokenModel tmodel;
            if (MTokens.ContainsKey(user))
            {
                tmodel = MTokens[user];
            }
            else
            {
                tmodel = new TokenModel();
                MTokens.Add(user, tmodel);
            }
            tmodel.LastOperateTime = DateTime.Now;
            tmodel.Tokenid = Guid.NewGuid().ToString();

            return tmodel.Tokenid;
        }
    }

    class TokenModel
    {
        public string Tokenid { get; set; }
        public DateTime LastOperateTime { get; set; }

    }

}