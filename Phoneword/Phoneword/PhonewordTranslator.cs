using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            raw = raw.ToUpperInvariant();

            // Creates a mutable string variable
            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if (" -0123456789".Contains(c))
                    newNumber.Append(c);
                else
                {
                    // Calls TranslateToNumber if char is not a number stated above
                    var result = TranslateToNumber(c);
                    if (result != null)
                        newNumber.Append(result);
                    // Bad character?
                    else
                        return null;
                }
            }
            return newNumber.ToString();
        }

        // Checks to see if a single char entered into the phone prompt
        // is a char in the passed in stringlist " -0123456789"
        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] digits =
        {
            // #2 on Keypad starts with string[0], # 1 = string[1], etc. 
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        static int? TranslateToNumber(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c))
                    // returns 2 bc 0 & 1 taken on keypad by special characters
                    return 2 + i;
            }
            return null;
        }
    }
}
