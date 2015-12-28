using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RootKit.Core
{
    //public partial class Extensions 
    public static partial class Extensions
    {
        private static string FilteredWords = @"[0-9]{4}p?|\[Tracker-surfer\]|\[Trackersurfer\]|Trackersurfer|truefrench|french|\.ld\.|\.hdtv\.|repack|multi|xvid|1cd|2cd|\.dvdrip\.|\.doc\.|\.blueray\.|x264|mkv|\.iso\.|\.avi\.|\.mov\.";
        /// <summary>
        /// Remove the file extension givent by arameter
        /// </summary>
        /// <param name="currentString">String to parse</param>
        /// <param name="ext">Extension list that need to be removed</param>
        /// <returns></returns>
        public static String RemoveFileExtension(this String currentString, String ext)
        {
            string[] exts = ext.Split(',');
            Regex extRegEx;

            foreach (string e in exts)
            {
                extRegEx = new Regex("." + e, RegexOptions.IgnoreCase);
                if (currentString.ToLower().Contains("." + e))
                {
                    currentString = extRegEx.Replace(currentString, "");
                    //currentString = currentString.Replace("." + e, "");
                }
            }
            return currentString.Trim();
        }

        /// <summary>
        /// Remove useless information from a movie name such as 
        /// \[Tracker-surfer\]|truefrench|french|ld|hdtv|repack|multi|xvid|1cd|2cd|dvdrip|blueray|x264|mkv|iso|avi|mov
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        public static String FilterMovieName(this String currentString)
        {
            Regex extRegEx;
            extRegEx = new Regex(FilteredWords, RegexOptions.IgnoreCase);

            Match toto = extRegEx.Match(currentString);
            if (toto.Index > 0) currentString = currentString.Remove(toto.Index);
            currentString = extRegEx.Replace(currentString, "");

            return currentString.Trim();
        }

        /// <summary>
        /// Remove useless information from a serie name such as 
        /// \[Tracker-surfer\]|truefrench|french|ld|hdtv|repack|multi|xvid|1cd|2cd|dvdrip|blueray|x264|mkv|iso|avi|mov
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        public static String FilterSerieName(this String currentString)
        {
            Regex extRegEx;
            
            extRegEx = new Regex(FilteredWords, RegexOptions.IgnoreCase);

            Match toto = extRegEx.Match(currentString);

            return currentString;
        }

        /// <summary>
        /// Reduce string to shorter preview which is optionally ended by some string (...).
        /// </summary>
        /// <param name="s">string to reduce</param>
        /// <param name="count">Length of returned string including endings.</param>
        /// <param name="endings">optional edings of reduced text</param>
        /// <example>
        /// string description = "This is very long description of something";
        /// string preview = description.Reduce(20,"...");
        /// produce -> "This is very long..."
        /// </example>
        /// <returns></returns>
        public static String Reduce(this string s, int count, string endings)
        {
            if (count < endings.Length)
                throw new Exception("Failed to reduce to less then endings length.");
            int sLength = s.Length;
            int len = sLength;
            if (endings != null)
                len += endings.Length;
            if (count > sLength)
                return s; //it's too short to reduce
            s = s.Substring(0, sLength - len + count);
            if (endings != null)
                s += endings;
            return s;
        }

        /// <summary>
        /// Reverse the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        /// returns a string with all words having first letter in "uppercase"
        /// </summary>
        /// <param name="inputString">the string object</param>
        /// <param name="forceLower">if true, force the string to lowercase</param>
        /// <returns></returns>
        public static String ToTitleCase(this string inputString, bool forceLower)
        {
            inputString = inputString.Trim();
            if (inputString == "")
            {
                return "";
            }
            if (forceLower)
            {
                inputString = inputString.ToLower();
            }

            string[] inputStringAsArray = inputString.Split(' ');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputStringAsArray.Length; i++)
            {
                if (inputStringAsArray[i].Length > 0)
                {
                    sb.AppendFormat("{0}{1} ",
                       inputStringAsArray[i].Substring(0, 1).ToUpper(),
                       inputStringAsArray[i].Substring(1));
                }
            }
            return sb.ToString(0, sb.Length - 1);
        }

        /// <summary>
        /// String conversion for generic interface IEnumerable<T>
        /// </summary>
        /// <typeparam name="T">Enumerator template</typeparam>
        /// <param name="source">Enumerator</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException("Parameter separator can not be null or empty.");

            string[] array = source.Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        /// <summary>
        /// String conversion for interface IEnumerable
        /// </summary>
        /// <param name="source">The enumerator value</param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToString(this IEnumerable source, string separator)
        {
            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException("Parameter separator can not be null or empty.");

            string[] array = source.Cast<object>().Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }


    }


}
