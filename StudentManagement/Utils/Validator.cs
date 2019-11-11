using StudentManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Utils
{
    public class Validator
    {
        private const int MaxLimit = 200;

        public static void ValidateId(string parameterName, int parameterValue)
        {
            if (parameterValue <= 0)
            {
                throw new InvalidParameterException($"Invalid id {parameterName} .");
            }
        }

        public static void RequiredNotNull(object parameter)
        {
            if (parameter == null)
            {
                throw new InvalidParameterException("Found null parameter.");
            }
        }

        public static void ValidateString(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new InvalidParameterException($"Invalid parameter {parameterName} .");
            }
        }

        public static void ValidateOffsetAndLimit(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new InvalidParameterException($"Invalid {nameof(offset)}.");
            }

            if (limit < 0)
            {
                throw new InvalidParameterException($"Invalid {nameof(limit)}.");
            }

            if (limit > MaxLimit)
            {
                throw new InvalidParameterException($"{nameof(limit)} extends max limit.");
            }
        }

        public static void ValidateOffsetAndLimitWithSize(int offset, int limit, int size)
        {
            if (offset >= size)
            {
                throw new InvalidParameterException($"{nameof(offset)} is out of range.");
            }
        }
    }
}
