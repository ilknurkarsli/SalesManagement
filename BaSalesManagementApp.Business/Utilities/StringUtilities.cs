using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Utilities
{
    public static class StringUtilities
    {
        private static readonly Dictionary<char, char> TurkishCharacterMap = new Dictionary<char, char>
    {
        {'i', 'İ'},
        {'ş', 'Ş'},
        {'ğ', 'Ğ'},
        {'ü', 'Ü'},
        {'ö', 'Ö'},
        {'ç', 'Ç'}
    };

        public static string CapitalizeEachWord(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CapitalizeFirstLetter(words[i]);
            }
            return string.Join(' ', words);
        }

        public static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            char firstChar = word[0];
            if (TurkishCharacterMap.ContainsKey(firstChar))
            {
                return TurkishCharacterMap[firstChar] + word.Substring(1);
            }
            return char.ToUpper(firstChar) + word.Substring(1).ToLower();
        }
    }

}