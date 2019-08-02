using MZcms.Common;
using System;

namespace MZcms.Common
{
    /// <summary>
    /// MZcms 异常
    /// </summary>
    public class MZcmsException : ApplicationException
    {
        public MZcmsException() {
            Log.Error(this.Message, this);
        }

        public MZcmsException(string message) : base(message) {
            Log.Error(message, this);
        }

        public MZcmsException(string message, Exception inner) : base(message, inner) {
            Log.Error(message, inner);
        }

    }
}
