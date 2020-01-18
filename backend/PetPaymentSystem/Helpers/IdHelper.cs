using System;

namespace PetPaymentSystem.Helpers
{
    public static class IdHelper
    {
        private static readonly object Locker = new object();
        private static int _counter;
        public static string GetSessionId()
            => ToUrlSafe(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

        public static string GetOperationId()
        {
            lock (Locker)
            {
                var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                var id = $"{timestamp}{_counter:D5}";
                _counter++;
                if (_counter % 100000 == 0) _counter = 0;
                return id;
            }
        }

        public static string GetMd() => new Guid().ToString();

        private static string ToUrlSafe(string str) 
            => str.Replace('+','.').Replace('/','_').Replace('=','-');

        private static string FromUrlSafe(string str)
            => str.Replace('.','+').Replace('_','/').Replace('-','=');
    }
}
