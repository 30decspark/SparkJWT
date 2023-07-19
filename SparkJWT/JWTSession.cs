using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkJWT
{
    public class JWTSession
    {
        private string? token;
        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public JWTSession(string? token)
        {
            if (token == null) return;
            this.token = token;
            JWT jwt = new JWT(token);
            dic = jwt.GetObject();
        }

        public string? GetString(string key)
        {
            if (token == null) return null;
            object? value;
            dic.TryGetValue(key, out value);
            if (value == null) return null;
            return (string)value;
        }

        public int? GetInt32(string key)
        {
            if (token == null) return null;
            object? value;
            dic.TryGetValue(key, out value);
            if (value == null) return null;
            return (int)value;
        }

        public decimal? GetDecimal(string key)
        {
            if (token == null) return null;
            object? value;
            dic.TryGetValue(key, out value);
            if (value == null) return null;
            return (decimal)value;
        }

        public bool? GetBoolean(string key)
        {
            if (token == null) return null;
            object? value;
            dic.TryGetValue(key, out value);
            if (value == null) return null;
            return (bool)value;
        }

        public object? GetObject(string key)
        {
            if (token == null) return null;
            object? value;
            dic.TryGetValue(key, out value);
            if (value == null) return null;
            return value;
        }

        public void SetString(string key, string value)
        {
            if (token == null) return;
            dic[key] = value;
        }

        public void SetInt32(string key, int value)
        {
            if (token == null) return;
            dic[key] = value;
        }

        public void SetDecimal(string key, decimal value)
        {
            if (token == null) return;
            dic[key] = value;
        }

        public void SetBoolean(string key, bool value)
        {
            if (token == null) return;
            dic[key] = value;
        }

        public void SetObject(string key, object value)
        {
            if (token == null) return;
            dic[key] = value;
        }

        public void Commit()
        {
            if (token == null) return;
            JWT jwt = new JWT(token);
            jwt.SetObject(dic);
        }
    }
}
