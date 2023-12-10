using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AnimalRefugeFinal.Models

{
    public static class SessionExtensions
    {
        // Extension method to set an object in session
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Extension method to get an object from session
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}

