using OnlineAccountDemo.Models;
using System.Text.Json;

namespace OnlineAccountDemo.Helper
{
    public class SessionService
    {
        public static string SessionKeyName = "LoggedInUser";
        internal static void SetSession(Users user, HttpContext httpContext)
        {
            httpContext.Session.Set<Users>(SessionKeyName, user);
        }

        internal static Users GetSession(HttpContext httpContext)
        {
            return httpContext.Session.Get<Users>(SessionKeyName);
        }
        internal static Users ClearSession(HttpContext httpContext)
        {
             httpContext.Session.Clear();
            return null;
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
