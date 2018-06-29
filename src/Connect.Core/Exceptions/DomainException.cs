using System;

namespace Connect.Core.Exceptions
{
    public class DomainException: Exception
    {
        public DomainException(string message = null)
            :base(message)
        {
        }
    }
}
