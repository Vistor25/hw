using System;
using System.Linq;

namespace Task2
{
    public static class ParseNumber
    {
        private const int OFFSET = 48;
        public static int Parse(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException();
            number = number.Trim();
            var isNegative = Negative(ref number);
            ValidateNumber(number);
            var y = 0;
            y = InternalParse(number, isNegative);
            return y;
        }

        private static bool Negative(ref string number)
        {
            if (number.StartsWith("-"))
            {
                number = number.Substring(1);
                return true;
            }
            if (number.StartsWith("+"))
            {
                number = number.Substring(1);
                return false;
            }
            return false;
        }

        private static int ToDigit(char a)
        {
            var b = Convert.ToByte(a);
            return b - OFFSET;
        }

        private static int InternalParse(string number, bool isNegative)
        {
            var result = 0;
            var counter = 1;
            for (var i = number.Length - 1; i >= 0; i--)
            {
                result = checked(result + ToDigit(number[i]) * counter);
                counter *= 10;
            }
            return isNegative ? result * -1 : result;
        }

        private static void ValidateNumber(string number)
        {
            if (number.Length > 10)
                throw new OverflowException();
            if (!number.All(item => Char.IsDigit(item)))
                throw new FormatException();
        }
    }
}