using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViannaWebCrawler
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException() { }
        public LoginFailedException(string message) : base(message) { }
        public LoginFailedException(string message, Exception inner) : base(message, inner) { }
        protected LoginFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
