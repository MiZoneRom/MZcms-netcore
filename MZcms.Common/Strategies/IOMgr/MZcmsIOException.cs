using System;
namespace MZcms.Common
{
	public class MZcmsIOException : MZcmsException
    {
		public MZcmsIOException()
		{
		}
		public MZcmsIOException(string message) : base(message)
		{
		}
        public MZcmsIOException(string message, Exception inner)
            : base(message, inner)
		{
		}
	}
}
