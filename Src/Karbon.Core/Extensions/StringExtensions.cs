using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karbon.Core
{
    public static class StringExtensions
    {
        public static string EnsureTrailingDirectorySeparator(this string path)
        {
            return EnsureTrailingCharacter(path, Path.DirectorySeparatorChar);
        }

        public static string EnsureTrailingForwardSlash(this string path)
        {
            return EnsureTrailingCharacter(path, '/');
        }

        public static string EnsureTrailingCharacter(this string input, char character)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            return input.Length == 0 || input[input.Length - 1] == character 
                ? input 
                : input + character;
        }

        public static string TrimStart(this string input, string toTrim)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            while (input.StartsWith(toTrim, StringComparison.InvariantCultureIgnoreCase))
                input = input.Substring(toTrim.Length);

            return input;
        }

        public static string TrimEnd(this string input, string toTrim)
        {
            if (string.IsNullOrEmpty(input)) 
                return input;

            while (input.EndsWith(toTrim, StringComparison.InvariantCultureIgnoreCase))
                input = input.Remove(input.LastIndexOf(toTrim, StringComparison.InvariantCultureIgnoreCase));

            return input;
        }

        public static bool IsAlphaNumeric(this string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]*$");
        }
    }
}
