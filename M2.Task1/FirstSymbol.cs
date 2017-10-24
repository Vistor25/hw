using System;

namespace Task1
{
    public static class FirstSymbol
    {
        public static char GetFirst(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException();
            }
            return str[0];
        }
    }
}