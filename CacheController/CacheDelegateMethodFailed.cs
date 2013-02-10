using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cache
{
    [Serializable]
    public class CacheDelegateMethodException : Exception
    {
        public CacheDelegateMethodException()
            : base()
        { }

        protected CacheDelegateMethodException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public CacheDelegateMethodException(string message)
            : base(message)
        {
           
        }

        public CacheDelegateMethodException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
