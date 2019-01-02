using System;
using System.Web;
using System.Web.Caching;

namespace MyLibrary
{
    public class CacheHelper
    {
        public static class DataCacheHelper
        {
            public static void Add<T>(T val, string key) where T : class
            {
                // Absolute Time
                HttpContext.Current.Cache.Insert(key, val, null, DateTime.Now.AddMinutes(3),
                System.Web.Caching.Cache.NoSlidingExpiration);

                // Sliding Time
                HttpContext.Current.Cache.Insert(key, val, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(120));

                // File Dependency
                CacheDependency cdep = new CacheDependency(HttpContext.Current.Server.MapPath("sample.xml"));
                HttpContext.Current.Cache.Insert(key, val, cdep, DateTime.Now.AddHours(3), System.Web.Caching.Cache.NoSlidingExpiration);

            }


            public static void Clear(string key)
            {
                HttpContext.Current.Cache.Remove(key);
            }


            public static bool Exists(string key)
            {
                return HttpContext.Current.Cache[key] != null;
            }

            public static T Get<T>(string key) where T : class
            {
                try
                {
                    return (T)HttpContext.Current.Cache[key];
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
