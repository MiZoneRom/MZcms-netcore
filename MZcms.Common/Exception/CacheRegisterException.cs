using System;

namespace MZcms.Common
{
    public class CacheRegisterException : MZcmsException
    {
        public CacheRegisterException() { }

        public CacheRegisterException(string message) : base(message) { }

        public CacheRegisterException(string message, Exception inner) : base(message, inner) { }
    }
}
