using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Tas2
{
    public static class ParseNumber
    {
        public static int Parse(string number)
        {
            if (String.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException();
            }
            number = number.Trim(' ');
            bool IsNegative = Negative(ref number);
            ValidateNumber(number);
            int y = 0;
            try
            {
               y = InternalParse(number, IsNegative);
            }
            catch (OverflowException)
            {

            }
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
            int b = Convert.ToByte(a);
            return b - 48;
        }

        private static int InternalParse(string number, bool isNegative)
        {
            int result = 0;
            int counter = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                result =checked(result + ToDigit(number[i]) * counter);
                counter *= 10;
            }
            return isNegative?result*-1:result;
        }
        private static void ValidateNumber(string number)
        {
           
            if (number.Length > 10)
            {
                throw new OverflowException();
            }
            if (number.All(item => !Char.IsDigit(item)))
            {
                throw new FormatException();
            }

        }
    }
}
