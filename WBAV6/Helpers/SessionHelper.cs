using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WBAV6.Models;

namespace WBAV6.Helpers
{
    public static class SessionHelper 
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static User GetUserSession(this ISession session)
        {
            var value = session.GetString("user");
            if (value == null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<User>(value);
        }
    }
}
