using System;

namespace Exceptions
{
    /// <summary>
    /// This exception is thrown if a object has an invalid tag.
    /// </summary>
    public class InvalidTagException : Exception
    {
        public InvalidTagException(string message) : base(message)
        {
        }

        public InvalidTagException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}