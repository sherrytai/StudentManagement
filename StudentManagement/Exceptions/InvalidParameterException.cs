using System;

namespace StudentManagement.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base(message)
        { }
    }
}
