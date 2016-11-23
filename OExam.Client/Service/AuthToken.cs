using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Client.Service
{
    /// <summary>
    /// TOKEN保存类
    /// </summary>
    class AuthToken
    {
        public static string TOKENNAME = "token";
        private static string _tokenValue = "";
        /// <summary>
        /// 获取已经保存的token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            return _tokenValue;
        }
        /// <summary>
        /// 保存token
        /// </summary>
        /// <param name="token"></param>
        public static void SaveToken(string token)
        {
            _tokenValue = token;
        }
    }
}
