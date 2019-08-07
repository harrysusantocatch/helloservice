using System;
using HelloService.Extension;

namespace HelloService.Helper
{
    public static class APIKeyGenerator
    {
        public static string Key => KeyGenerator();
        public static string LastKey => LastKeyGenerator();
        public static string LastWeekKey => LastWeekKeyGenerator();
        public static string LastLastWeekKey => LastLastWeekKeyGenerator();

        static string KeyGenerator()
        {
            string phrase = Startup.Configuration["Hello:KeyPhrase"];
            var week = DateTime.Now.ToUnixLong() / 604800000;
            return KeyGenerator(phrase, week);
        }

        static string LastKeyGenerator()
        {
            string phrase = Startup.Configuration["Hello:KeyPhrase"];
            var week = DateTime.Now.ToUnixLong() / 604800000;
            return KeyGenerator(phrase, week - 1);

        }

        static string LastWeekKeyGenerator()
        {
            string phrase = Startup.Configuration["Hello:LastKeyPhrase"];
            var week = DateTime.Now.ToUnixLong() / 604800000;
            return KeyGenerator(phrase, week);

        }

        static string LastLastWeekKeyGenerator()
        {
            string phrase = Startup.Configuration["Hello:LastKeyPhrase"];
            var week = DateTime.Now.ToUnixLong() / 604800000;
            return KeyGenerator(phrase, week - 1);

        }

        static string KeyGenerator(string phrase, long week)
        {
            string key = "";
            for (int i = 0; i < phrase.Length; i++)
            {
                long ascii = phrase[i];
                Int32 divisor = week % 2 == 0 ? i + 1 : phrase.Length - i;
                long divided = ascii * week * phrase.Length;
                Int32 intKey = (int)(divided / divisor);
                key += intKey.ToString("X");
            }
            key = key.ToLower();
            if (phrase.Length % 2 == 0)
            {
                var charArr = key.ToCharArray();
                Array.Reverse(charArr);
                key = new string(charArr);
            }
            return key;
        }

        public static bool IsKeyValid(string key)
        {
            return key == Key || key == LastKey || key == LastWeekKey || key == LastLastWeekKey;
        }
    }
}
