namespace SparkJWT
{
    public class JWT
    {
        private string? token;

        public JWT(){ }

        public JWT(string? token)
        {
            this.token = token;
        }

        public string Token()
        {
            string token = Guid.NewGuid().ToString("N");
            SessionStore.Add(token, (DateTime.Now, new Dictionary<string, object>()));
            if (this.token == null) this.token = token;
            return token;
        }

        public bool CheckAndUpdate()
        {
            if (token == null) return false;
            (DateTime, Dictionary<string, object>) session;
            SessionStore.TryGetValue(token, out session);
            if (session == (null, null)) return false;
            if (session.Item1 < DateTime.Now.AddMinutes(-JWT.ExpiryTimeOut))
            {
                SessionStore.Remove(token);
                SessionStore.TrimExcess();
                return false;
            }
            session.Item1 = DateTime.Now;
            SessionStore[token] = session;
            return true;
        }

        public void Delete()
        {
            if (token == null) return;
            SessionStore.Remove(token);
            SessionStore.TrimExcess();
        }

        public void ClearExpires()
        {
            foreach (string token in SessionStore.Keys)
            {
                (DateTime, Dictionary<string, object>) session;
                SessionStore.TryGetValue(token, out session);
                if (session == (null, null)) continue;
                if (session.Item1 < DateTime.Now.AddMinutes(-JWT.ExpiryTimeOut)) SessionStore.Remove(token);
            }
            SessionStore.TrimExcess();
        }

        public Dictionary<string, object> GetObject()
        {
            if (token == null) return new Dictionary<string, object>();
            (DateTime, Dictionary<string, object>) session;
            SessionStore.TryGetValue(token, out session);
            if (session == (null, null)) return new Dictionary<string, object>();
            if (session.Item1 < DateTime.Now.AddMinutes(-JWT.ExpiryTimeOut)) return new Dictionary<string, object>();
            session.Item1 = DateTime.Now;
            return session.Item2;
        }

        public void SetObject(Dictionary<string, object> dic)
        {
            if (token == null) return;
            (DateTime, Dictionary<string, object>) session;
            SessionStore.TryGetValue(token, out session);
            if (session == (null, null)) return;
            if (session.Item1 < DateTime.Now.AddMinutes(-JWT.ExpiryTimeOut)) return;
            session.Item1 = DateTime.Now;
            session.Item2 = dic;
            SessionStore[token] = session;
        }

        public static int ExpiryTimeOut { get; set; } = 20; //minute
        private static Dictionary<string, (DateTime, Dictionary<string, object>)> SessionStore = new Dictionary<string, (DateTime, Dictionary<string, object>)>();
    }
}