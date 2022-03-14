using LLM.Store.ApplicationCore.Entities;
using LLM.Store.ApplicationCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace LLM.Store.ApplicationCore.Utils
{
    public static class CommonExtensions
    {
        #region Object extension methods
        /// <summary>
        /// Returns TRUE if an objects value is <cref="System.null" /> and FALSE if the object has been initiated.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this object o)
        {
            return o == null;
        }
        public static bool IsNullOrValue(this int? value, int valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        public static bool IsNullOrValue(this double? value, double valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        public static bool IsNullOrValue(this decimal? value, decimal valueToCheck)
        {
            return (value ?? valueToCheck) == valueToCheck;
        }
        #endregion Object extension methods

        #region String extension methods
        public static string GetLast(this string source, int numberOfChars)
        {
            if (numberOfChars >= source.Length)
                return source;
            return source.Substring(source.Length - numberOfChars);
        }
        /// <summary>
        /// Replaces the format item in a specified System.String with the text 
        /// equivelent of the value from a corresponding System.Object reference in a specified array
        /// </summary>
        /// <param name="formatProvider">a composite format string</param>
        /// <param name="t">An System.Object to format</param>
        /// <exception cref="System.ArgumentNullException" />
        /// <exception cref="System.FormatException" />
        /// <returns>a compiled string</returns>
        public static string Combine(this string formatProvider, params object[] args)
        {
            return String.Format(formatProvider, args);
        }

        /// <summary>
        /// Executes a passed Regular Expression against a string and returns the result of the match
        /// </summary>
        /// <param name="s">the target string</param>
        /// <param name="RegEx">a System.String containing a regular expression</param>
        /// <returns>true if the regular expression matches the target string and false if it does not</returns>
        public static bool IsMatch(this string s, string RegEx)
        {
            return new Regex(RegEx).IsMatch(s);
        }

        /// <summary>
        /// Executes a passed Regular Expression against a string and returns the result of the match
        /// </summary>
        /// <param name="s">the target string</param>
        /// <param name="RegEx">any Regular Expression object</param>
        /// <returns>true if the regular expression matches the target string and false if it does not</returns>
        public static bool IsMatch(this string s, Regex RegEx)
        {
            return RegEx.IsMatch(s);
        }

        /// <summary>
        /// Checks to see if the string contains a valid email address.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>Returns TRUE if the string contains an email address and it is valid or FALSE if the string 
        /// has no email address or it is invalid</returns>
        public static bool IsValidEmail(this string s)
        {
            return s.IsMatch(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
        /// <summary>
        /// Checks to see if the string contains a valid email address.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>Returns TRUE if the string contains an email address and it is valid or FALSE if the string 
        /// has no email address or it is invalid</returns>
        public static bool IsValidPhone(this string s)
        {
            if (s == null)
            {
                return false;
            }
            else
            {
                
                return s.IsMatch(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
               // return s.IsMatch(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            }
        }

        /// <summary>
        /// Checks to see if the string is all numbers
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>Returns TRUE if the string is all numbers or FALSE if the string has no numbers or is all alpha characters.</returns>
        public static bool IsInteger(this string s)
        {
            return !s.IsMatch("[^0-9-]") && s.IsMatch("^-[0-9]+$|^[0-9]+$");
        }

        /// <summary>
        /// Tests a string to see if it is a Positive Integer
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNaturalNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9]") && strNumber.IsMatch("0*[1-9][0-9]*");
        }

        /// <summary>
        /// Tests a string to see if it is a Positive Integer with zero inclusive
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9]");
        }

        /// <summary>
        /// Tests a string to see if it a Positive number both Integer & Real
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsPositiveNumber(this string strNumber)
        {
            return !strNumber.IsMatch("[^0-9.]") && strNumber.IsMatch("^[.][0-9]+$|[0-9]*[.]*[0-9]+$") && !strNumber.IsMatch("[0-9]*[.][0-9]*[.][0-9]*");
        }

        /// <summary>
        /// Tests a string to see if it is a number
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(this string strNumber)
        {
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            return !strNumber.IsMatch("[^0-9.-]") && !strNumber.IsMatch("[0-9]*[.][0-9]*[.][0-9]*") &&
                !strNumber.IsMatch("[0-9]*[-][0-9]*[-][0-9]*") && strNumber.IsMatch("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
        }

        /// <summary>
        /// Tests a string to see if it contains alpha characters
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool IsAlpha(this string strToCheck)
        {
            return !strToCheck.IsMatch("[^a-zA-Z]");
        }

        /// <summary>
        /// Tests a string to see if it contains alpha and numeric characters
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string strToCheck)
        {
            return !strToCheck.IsMatch("[^a-zA-Z0-9]");
        }

        /// <summary>
        /// Checks to see if a System.String value is a string representation of a boolean value.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <returns>true if the System.String value is truthy or false if not</returns>
        public static bool IsBoolean(this string s)
        {
            string _lower = s.ToLower();
            return _lower.IsMatch("[true]|[false]|[0]|[1]");
        }

        /// <summary>
        /// Replicates a System.String the passed number of times and returns the value
        /// </summary>
        /// <param name="s">the target System.String</param>
        /// <param name="times">the number of times to replicate the string</param>
        /// <returns>the replicated string</returns>
        public static string Replicate(this string s, int times)
        {
            string hold = String.Empty;

            times.Times(delegate (int i)
            {
                hold += s;
            });

            return hold;
        }

        /// <summary>
        /// Converts a System.String value to a boolean type.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns the boolean representation of the strings value or FALSE if the string is not a valid boolean.</returns>
        /// <exception cref="System.FormatException" />
        /// <remarks>Calls extension IsBoolean()</remarks>
        public static bool InterpretAsBoolean(this string s)
        {
            if (s.IsBoolean())
            {
                return Convert.ToBoolean(s);
            }
            return false;
        }

        /// <summary>
        /// Converts a System.String value to an integer type.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns the integer representation of the strings value or -1 if the string is not a valid integer.</returns>
        /// <exception cref="System.FormatException" />
        /// <remarks>Calls extension IsNumeric()</remarks>
        public static int InterpretAsInteger(this string s)
        {
            if (s.IsInteger())
            {
                return Convert.ToInt32(s);
            }
            return -1;
        }

        /// <summary>
        /// Converts a System.String to proper case.
        /// </summary>
        /// <param name="s">the target string</param>
        /// <exception cref="System.FormatException" />
        /// <returns>a properly cased System.String</returns>
        public static string Capitalize(this string s)
        {
            string result = String.Empty;
            foreach (string p in s.Split(' '))
            {
                if (p != String.Empty)
                    result += p.Substring(0, 1).ToUpper() + p.Substring(1) + " ";
            }
            return result;
        }

        /// <summary>
        /// Searches a given System.String for the next to last occurance of another System.String
        /// </summary>
        /// <param name="s">the string to search</param>
        /// <param name="index">the string to look for</param>
        /// <returns>the position of the occurance</returns>
        public static int NextToLastIndexOf(this string s, string index)
        {
            int i = 0;
            int end = s.LastIndexOf(index);

            if (end == -1) return end;

            do
            {
                i = s.IndexOf(index);
            }
            while (i > -1 && i < end);

            return i;
        }



        /// <summary>
        /// Allows iterating over every occurrence of the given pattern (which can be a string or a regular expression). Returns the original string.
        /// </summary>
        /// <param name="s">the strig to scan</param>
        /// <param name="RegularExpression">The pattern to match against</param>
        /// <param name="Delegate">function delegate that will process the matched items</param>
        /// <returns>The original string</returns>
        public static string Scan(this string s, Regex RegularExpression, Action<string> Delegate)
        {
            if (RegularExpression.IsMatch(s))
            {
                MatchCollection mc = RegularExpression.Matches(s, 0);
                foreach (Match m in mc)
                {
                    Delegate(m.Value);
                }
            }

            return s;
        }

        /// <summary>
        /// Checks if a string is empty or null. Returns false if the string has data
        /// </summary>
        /// <param name="s">the string to check</param>
        /// <returns>false if the string has data</returns>
        public static bool HasValue(this string s)
        {
            //if (!s.IsNull() || s.Trim().Equals(String.Empty))
            if (s.IsNull() || string.IsNullOrEmpty(s))
            {
                return false;
            }
            return true;
        }
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return (guid == Guid.Empty);
        }
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return (!guid.HasValue || guid.Value == Guid.Empty);
        }
        /// <summary>
        /// Checks if a string has any trailing or leading whitespace
        /// </summary>
        /// <param name="s">the string to check</param>
        /// <returns>true if the string can be trimmed</returns>
        public static bool CanBeTrimmed(this string s)
        {
            return !s.Equals(s.Trim());
        }
        //---------------------------------------------------------------------------------
        // Procedure Name - get string Value
        // Procedure Type - User Defined Function
        // Return Type - string
        // Parameters - objTest: Object
        // Description - This is the User Defined Function for retreiving string value
        //---------------------------------------------------------------------------------
        public static string ToGetStringValue(this object objTest)
        {
            try
            {
                return (objTest != null) && (!object.ReferenceEquals(objTest, System.DBNull.Value)) ? objTest.ToString() : "";
            }
            catch (Exception)
            {
            }
            return "";
        }

        #endregion String extension methods

        #region Timespan extension methods
        /// <summary>
        /// Determines a date of time from a timespan
        /// </summary>
        /// <param name="val">the target timespan</param>
        /// <example>
        /// TimeSpan.FromHours(2.0).Ago(); //two hours in the past
        /// </example>
        /// <returns>a datetime object</returns>
        public static DateTime Ago(this TimeSpan val)
        {
            return DateTime.Now.Subtract(val);
        }
        /// <summary>
        /// Determines a date of time from a timespan
        /// </summary>
        /// <param name="val">the target timespan</param>
        /// <example>
        /// TimeSpan.FromHours(2.0).FromNow(); //two hours in the future
        /// </example>
        /// <returns>a datetime object</returns>
        public static DateTime FromNow(this TimeSpan val)
        {
            return DateTime.Now.Add(val);
        }
        #endregion Timespan extension methods

        #region Integer extension methods

        /// <summary>
        /// Format integer with commas (i.e. 5,000).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatWithCommas(this decimal value)
        {
            // n0 because there will never be decimal places for an integer.
            return string.Format("{0:n0}", value);
        }
        public static string FormatWithCommas(this decimal value, int decimalPlaces)
        {
            var format = "{0:n" + decimalPlaces.ToString() + "}";

            return string.Format(format, value);
        }
        public static string FormatWithCommas(this double value)
        {
            // n0 because there will never be decimal places for an integer.
            return string.Format("{0:n0}", value);
        }
        public static string FormatWithCommas(this double value, int decimalPlaces)
        {
            var format = "{0:n" + decimalPlaces.ToString() + "}";

            return string.Format(format, value);
        }
        public static bool IsEven(this int i)
        {
            return ((i % 2) == 0);
        }

        public static bool IsPrimeNumber(this int i)
        {
            if (i == 1) return false;
            if (i == 2) return true;

            double boundry = Math.Floor(Math.Sqrt(i));

            for (int j = 2; j <= boundry; ++j)
            {
                if ((i % j) == 0) return false;
            }

            return true;
        }
        /// <summary>
        ///  converts an object to boolean value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>bool</returns>
        public static bool ToBoolean(this object value)
        {
            bool retValue = false;

            if (value != DBNull.Value)
            {
                try
                {
                    value = Convert.ToBoolean(value);
                }
                catch { }
                bool.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        public static int ToInteger(this bool value)
        {
            if (value == true)
                return 1;
            else
                return 0;
        }
        /// <summary>
        ///  converts an object to integer value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int</returns>
        public static int ToInteger(this object value)
        {
            int retValue = 0;

            if (value != DBNull.Value && value != null)
            {
                int.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        public static string ToValidString(this object value)
        {
            return (value == null) ? string.Empty : Convert.ToString(value);
        }
        //public static Guid ToGuid(this object value)
        //{
        //    Guid guid;

        //    if (value != DBNull.Value)
        //    {
        //        Guid.TryParse(Convert.ToString(value), out guid);
        //    }
        //    return guid;
        //}
        public static decimal ToDecimal(this object value)
        {
            decimal retValue = 0;

            if (value != DBNull.Value)
            {
                decimal.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        /// <summary>
        /// converts an object to double value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>double</returns>
        public static double ToDouble(this object value)
        {
            double retValue = 0;

            if (value != DBNull.Value)
            {
                double.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        /// <summary>
        ///  converts an object to integer value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>int</returns>
        public static long ToLong(this object value)
        {
            long retValue = 0;

            if (value != DBNull.Value)
            {
                long.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        /// <summary>
        /// converts an object to float
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>float</returns>
        public static float ToFloat(this object value)
        {
            float retValue = 0;

            if (value != DBNull.Value)
            {
                float.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        /// <summary>
        /// Executes a delegate method the specified number of times
        /// </summary>
        /// <param name="i">the number of times to execute the delegate</param>
        /// <param name="function">the delegate to execute</param>
        public static void Times(this int i, Action function)
        {
            for (int ii = 0; ii <= i - 1; ii++)
            {
                function();
            }
        }

        /// <summary>
        /// Executes a delegate method the specified number of times and passes the iteration counter to the delegate
        /// </summary>
        /// <param name="i">the number of times to execute the delegate</param>
        /// <param name="function">the delegate to execute</param>
        public static void Times(this int i, Action<int> function)
        {
            for (int ii = 0; ii <= i - 1; ii++)
            {
                function(ii);
            }
        }

        /// <summary>
        /// Converts a number to it's comma seperated representation
        /// </summary>
        /// <param name="i">the number to convert</param>
        /// <returns>a System.String containing the comma seperated representation</returns>
        public static string Commaify(this int i)
        {
            string number = i.ToString();
            string natLangNum = String.Empty;
            string numberPieces = String.Empty;
            int backPos = 0, counter = 1;
            for (int ii = 0; ii <= number.Length - 1; ii++)
            {
                backPos = number.Length - ii;

                numberPieces = number.Substring(backPos - 1, 1) + numberPieces;

                if (counter % 3 == 0)
                {
                    natLangNum += "," + numberPieces;
                    numberPieces = String.Empty;
                    counter = 1;
                }
                else
                {
                    counter++;
                }
            }

            if (numberPieces != String.Empty)
                natLangNum += "," + numberPieces;

            return natLangNum.Substring(1);
        }

        /// <summary>
        /// Converts a number to it's natural language equivelent
        /// </summary>
        /// <param name="i">the number to convert</param>
        /// <returns>a System.String containing the converted representation</returns>
        public static string ToNaturalLanguage(this int i)
        {
            if (i == 0) return "zero";

            string[] int2str1 = new string[] { String.Empty, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] int2str2 = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] int2str3 = new string[] { String.Empty, String.Empty, "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] int2str4 = new string[] { "hundred", "thousand", "million", "billion", "trillion", "quadrillion" };

            string natLangNum = String.Empty;

            if (i <= int2str1.Length - 1) return int2str1[i];
            if (i > 9 && i < 20) return int2str2[i - 10];

            string number = i.ToString();
            string[] Arr = i.Commaify().Split(',');

            for (int ii = Arr.Length - 1; ii >= 0; --ii)
            {
                string result = String.Empty, piece = Arr[ii], suffix = int2str4[Arr.Length];

                if (piece.Length == 3 && Arr.Length >= 1)
                {
                    int firstPos = piece.Substring(0, 1).InterpretAsInteger(); // Convert.ToInt32(piece.Substring(0, 1));
                    int secondPos = piece.Substring(1, 1).InterpretAsInteger(); // Convert.ToInt32(piece.Substring(1, 1));
                    int thirdPos = piece.Substring(2, 1).InterpretAsInteger(); // Convert.ToInt32(piece.Substring(2, 1));
                    int secondthirdPos = piece.Substring(1, 2).InterpretAsInteger(); // Convert.ToInt32(piece.Substring(1, 2));

                    string firstPosStr = firstPos == 0 ? String.Empty : int2str1[firstPos] + " " + int2str4[0];
                    string secondPosStr = secondPos == 0 ? String.Empty : int2str1[secondPos] == String.Empty ? int2str2[(secondthirdPos < 11 ? secondthirdPos + 10 : secondthirdPos) - 10] : int2str3[secondPos];
                    string thirdPosStr = thirdPos == 0 ? String.Empty : int2str1[secondPos] == String.Empty ? String.Empty : "-" + int2str1[thirdPos];
                    result = firstPosStr.Trim() + (secondPosStr != String.Empty ? " " : String.Empty) + secondPosStr.Trim() + thirdPosStr.Trim() + " " + (ii == 0 ? String.Empty : int2str4[ii].Trim()) + " ";
                }
                else if (piece.Length < 3 && Arr.Length > 1 && ii > 0)
                {
                    int firstPos = Convert.ToInt32(piece);
                    string firstPosStr = firstPos < 10 ? int2str1[firstPos] : firstPos > 19 ? int2str3[firstPos / 10] : int2str2[firstPos - 10];
                    result = firstPosStr.Trim() + " " + int2str4[ii].Trim() + " ";
                }

                natLangNum += result;
            }

            return natLangNum.Trim();
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Minutes
        /// </summary>
        /// <param name="i">the number of minutes to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Minutes(this int i)
        {
            return new TimeSpan(0, i, 0);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Hours
        /// </summary>
        /// <param name="i">the number of hours to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Hours(this int i)
        {
            return new TimeSpan(i, 0, 0);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Seconds
        /// </summary>
        /// <param name="i">the number of seconds to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Seconds(this int i)
        {
            return new TimeSpan(0, 0, i);
        }

        /// <summary>
        /// Generates a TimeSpan representing a set number of Days
        /// </summary>
        /// <param name="i">the number of days to set in the timespan</param>
        /// <returns>a System.TimeSpan object</returns>
        public static TimeSpan Days(this int i)
        {
            return new TimeSpan(i, 0, 0, 0);
        }
        #endregion Integer extension methods
        #region List
        public static void AddToFront<T>(this List<T> list, T item)
        {
            // omits validation, etc.
            list.Insert(0, item);
        }
        #endregion List

        #region Date
        /// <summary>
        ///  converts an object to datetime value
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>System.DateTime</returns>
        public static DateTime ToDateTime(this object value)
        {
            DateTime retValue = new DateTime();

            if (value != DBNull.Value)
            {
                DateTime.TryParse(value.ToString(), out retValue);
            }

            return retValue;
        }
        public static DateTime StartDateOfTheWeek(this DateTime dt)
        {
            DateTime _returnDateTime = dt.AddDays(-((dt.DayOfWeek - Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)));
            return _returnDateTime;
        }
        public static DateTime EndDateOfCurrentWeek(this DateTime dt)
        {
            return dt.StartDateOfTheWeek().AddDays(6);
        }
        public static DateTime StartDateOfTheMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime StartDateOfTheMonth(this int year,int month)
        {
            return new DateTime(year, month, 1);
        }
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
        public static string ToMonthName(this int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        public static string ToShortMonthName(this int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
        }
        public static DateTime EndDateOfCurrentMonth(this DateTime dt)
        {
            return DateTime.Now.StartDateOfTheMonth().AddDays(DateTime.DaysInMonth(dt.Year, dt.Month) - 1);
        }
        public static string ToDateFormat(this DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToString("dd-MMM-yyyy");
            return string.Empty;
        }
        public static string ToDateTimeFormat(this DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToString("dd-MMM-yyyy hh:mm tt");
            return string.Empty;
        }
        public static string ToTimeFormat(this DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToString("hh:mm tt");
            return string.Empty;
        }
        public static string ToTimeFormat(this DateTime date)
        {
            try
            {
                DateTime retValue = new DateTime();
                if (date != null)
                {
                    DateTime.TryParse(date.ToString(), out retValue);
                    return retValue.ToString("hh:mm tt");
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }
        public static string ToDateFormat(this DateTime date)
        {
            try
            {
                DateTime retValue = new DateTime();
                if (date != null)
                {
                    DateTime.TryParse(date.ToString(), out retValue);
                    return retValue.ToString("dd-MMM-yyyy");
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }
        public static string ToDateForJson(this DateTime date)
        {
            try
            {
                DateTime retValue = new DateTime();
                if (date != null)
                {
                    DateTime.TryParse(date.ToString(), out retValue);
                    return retValue.ToString();
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }
        public static string ToDateTimeFormat(this DateTime date)
        {
            try
            {
                if (date != DateTime.MinValue)
                {
                    DateTime retValue = new DateTime();
                    if (date != null)
                    {
                        DateTime.TryParse(date.ToString(), out retValue);
                        return retValue.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                    else
                    {
                        return "N/A";
                    }
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }
        public static string ToDateTimeFormat(this DateTime date,string Format)
        {
            try
            {
                if (date != DateTime.MinValue)
                {
                    DateTime retValue = new DateTime();
                    if (date != null)
                    {
                        DateTime.TryParse(date.ToString(), out retValue);
                        return retValue.ToString(Format);
                    }
                    else
                    {
                        return "N/A";
                    }
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }
        public static string ToDefaultNa(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                  return value.Trim();
                }
                else
                {
                    return "NA";
                }
            }
            catch
            {
                return "NA";
            }
        }
        #endregion Date

        #region UrlFriendly
        public static string ToUrlFriendly(this string name)
        {
            // Fallback for product variations
            if (string.IsNullOrWhiteSpace(name))
            {
                return Guid.NewGuid().ToString();
            }

            name = name.ToLower();
            name = RemoveDiacritics(name);
            name = ConvertEdgeCases(name);
            name = name.Replace(" ", "-");
            name = name.Strip(c =>
                c != '-'
                && c != '_'
                && !c.IsLetter()
                && !Char.IsDigit(c)
                );

            while (name.Contains("--"))
                name = name.Replace("--", "-");

            if (name.Length > 200)
                name = name.Substring(0, 200);

            if (string.IsNullOrWhiteSpace(name))
            {
                return Guid.NewGuid().ToString();
            }

            return name;
        }

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsLetter(this char c)
        {
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
        }

        public static bool IsSpace(this char c)
        {
            return (c == '\r' || c == '\n' || c == '\t' || c == '\f' || c == ' ');
        }

        public static string Strip(this string subject, params char[] stripped)
        {
            if (stripped == null || stripped.Length == 0 || String.IsNullOrEmpty(subject))
            {
                return subject;
            }

            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (Array.IndexOf(stripped, current) < 0)
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static string Strip(this string subject, Func<char, bool> predicate)
        {

            var result = new char[subject.Length];

            var cursor = 0;
            for (var i = 0; i < subject.Length; i++)
            {
                char current = subject[i];
                if (!predicate(current))
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        private static string ConvertEdgeCases(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                sb.Append(ConvertEdgeCases(c));
            }

            return sb.ToString();
        }

        private static string ConvertEdgeCases(char c)
        {
            string swap;
            switch (c)
            {
                case 'ı':
                    swap = "i";
                    break;
                case 'ł':
                case 'Ł':
                    swap = "l";
                    break;
                case 'đ':
                    swap = "d";
                    break;
                case 'ß':
                    swap = "ss";
                    break;
                case 'ø':
                    swap = "o";
                    break;
                case 'Þ':
                    swap = "th";
                    break;
                default:
                    swap = c.ToString();
                    break;
            }

            return swap;
        }

        #endregion UrlFriendly

        #region Http
        public static string HtmlEncode(this string data)
        {
            return HttpUtility.HtmlEncode(data);
        }

        public static string HtmlDecode(this string data)
        {
            return HttpUtility.HtmlDecode(data);
        }
        public static NameValueCollection ParseQueryString(this string query)
        {
            return HttpUtility.ParseQueryString(query);
        }
        public static string UrlPathEncode(this string url)
        {
            return HttpUtility.UrlPathEncode(url);
        }
        // Extension to replace spaces with &nbsp;
        public static string SpaceToNbsp(this string s)
        {
            return s.Replace(" ", "&nbsp;");
        }

        // Url encode an ASCII string.
        public static string UrlEncode(this string s)
        {
            return HttpUtility.UrlEncode(s);
        }

        // Url decode an ASCII string.
        public static string UrlDecode(this string s)
        {
            return HttpUtility.UrlDecode(s);
        }
        #endregion Http

        #region Sql
        public static string ToSqlFormat(this object data)
        {
            try
            {
                if (!string.IsNullOrEmpty(ToGetStringValue(data)))
                    return (data != null) && (!object.ReferenceEquals(data, System.DBNull.Value)) ? data.ToString().Replace("'", "''") : "";
            }
            catch (Exception)
            {

            }

            return "";
        }
        #endregion Sql

        #region IEnumerable
        /// <summary>
        /// Converts a IEnumerable<T> value to a CommaSeparate .
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Comma Separate Value</returns>
        /// <remarks>Calls extension:- string commas = users.CommaSeparate( u => u.UserId );</remarks>
        public static string CommaSeparate<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(",", source.Select(s => func(s).ToString()).ToArray());
        }
        /// <summary>
        /// Converts a IEnumerable<string> value to a CommaSeparate .
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Comma Separate Value</returns>
        /// <remarks>Calls extension:- strings.CommaSeparate(",")</remarks>
        public static string CommaSeparate(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }
        #endregion IEnumerable

        #region Custom
        public static string ReturnStatus(this int status)
        {
            string msg = string.Empty;
            switch (status)
            {
                case 0:
                    msg = "Something went wrong";
                    break;
                case 1:
                    msg = "Successfully Inserted";
                    break;
                case 2:
                    msg = "Successfully Updated";
                    break;
                case 3:
                    msg = "Already processed";
                    break;
                case 4:
                    msg = "Pending state";
                    break;
                case 5:
                    msg = "Cancelled";
                    break;
                case 6:
                    msg = "Successfully approved";
                    break;
                case 7:
                    msg = "Customer credit not available";
                    break;
                case 8:
                    msg = "User credit not available";
                    break;
                case -1:
                    msg = "Duplicate Record";
                    break;
                case 200:
                    msg = "Successful";
                    break;
                case -100:
                    msg = "An error occurred while processing your request(rollback failed optional)";
                    break;
            }
            return msg;
        }
        public static string ToFormatWithCode(this string code, string name)
        {
            if (string.IsNullOrEmpty(code))
            {
                return "N/A";
            }
            else
            {
                return "[" + code + "]" + name;
            }
        }

        #endregion
        public static string CompareDate(string date1,string date2)
        {
            string returnvalue = date1;
            if (!string.IsNullOrEmpty(date2))
            {
                DateTime dt1 = Convert.ToDateTime(date1);
                DateTime dt2 = Convert.ToDateTime(date2);
                int value = DateTime.Compare(dt1, dt2);
                if(value < 0)
                {
                    returnvalue = date2;
                }
            }
            return returnvalue;
        }
        public static string GetExcelColumn(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        public static TokenInfo GetTokenInfo(this ClaimsPrincipal principal)
        {
            TokenInfo tokenInfo = new TokenInfo()
            {
                created_by = (principal?.Claims?.SingleOrDefault(p => p.Type == "created_by")?.Value).ToInteger(),
                modified_by = (principal?.Claims?.SingleOrDefault(p => p.Type == "modified_by")?.Value).ToInteger(),
                //UserName = (principal?.Claims?.SingleOrDefault(p => p.Type == "UserName")?.Value).ToString(),
                //OperationLocationIds = (principal?.Claims?.SingleOrDefault(p => p.Type == "OperationLocationIds")?.Value).ToValidString(),
                //CustomerLocationIds = (principal?.Claims?.SingleOrDefault(p => p.Type == "CustomerLocationIds")?.Value).ToValidString()
            };
            return tokenInfo;
        }
    }
}
