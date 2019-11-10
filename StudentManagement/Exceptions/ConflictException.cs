using System;

namespace StudentManagement.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        { }
    }
}
