using StudentManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static void ValidateName(string parameterName, string parameterValue)
        {
            ValidateString(parameterName, parameterValue);
            if (!parameterValue.All(c => char.IsLetterOrDigit(c)))
            {
                throw new InvalidParameterException($"{parameterName} can only contain letter or digit .");
            }
        }

        public static void ValidateEmail(string parameterName, string parameterValue)
        {
            ValidateString(parameterName, parameterValue);
            if (!IsValidEmail(parameterValue))
            {
                throw new InvalidParameterException($"{parameterName} is not valid email .");
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

        private static bool IsValidEmail(string email)
        {
            var regex = new Regex("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$");
            var result = regex.Match(email);
            return result.Success;
        }
    }
}
