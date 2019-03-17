using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication2.Utils
{
    /// <summary>
    /// token辅助类
    /// </summary>
    public class TokenHelper
    {
        /// <summary>
        /// token http头前缀
        /// </summary>
        public const string HEADER_PREFIX = "Authorization";
        /// <summary>
        /// token类型前缀
        /// </summary>
        public const string TOKEN_PREFIX = "Bearer ";
        /// <summary>
        /// 缓存对象
        /// </summary>
        static ICache cache = new RuntimeCache();
        /// <summary>
        /// 生成token，并存入缓存里
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GeneratorToken(string username)
        {
            #region 生成token并返回
            string token = Guid.NewGuid().ToString();
            cache.Put(token, username, 24 * 60);//超时1天
            #endregion
            return token;
        }
        /// <summary>
        /// 教研token的正确性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateToken(string token)
        {
            token = token.Substring(TOKEN_PREFIX.Length);
            return cache.Get(token) != null;
        }
    }
}