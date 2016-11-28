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
        /// 令牌信息
        /// </summary>
        public static string Token
        {
            get
            {
                return _tokenValue;
            }
            set
            {
                _tokenValue = value;
            }
        }

    }
}
