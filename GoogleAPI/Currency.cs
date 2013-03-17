using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace GoogleAPI
{
    /// <summary>
    ///     Utility class to convert currencies
    ///     Methods available to get the result as a decimal or string
    ///     Ryan Harrison 2013
    ///     www.ryanharrison.co.uk
    /// </summary>
    public static class Currency
    {
        /// <summary>
        ///     Base Url for use when obtaining the conversion
        /// </summary>
        private const string BASEURL = "http://www.google.com/ig/calculator?hl=en&q={0}{1}=?{2}";

        /// <summary>
        ///     Converts a value from one currency to another, returning the value as a string e.g "12.45 GBP"
        /// </summary>
        /// <param name="amount">The amount to convert</param>
        /// <param name="from">The currency code to convert from</param>
        /// <param name="to">The currency code to convert to</param>
        /// <returns>A string representation of the result e.g "12.45 GBP"</returns>
        public static string ConvertToString(decimal amount, string from, string to)
        {
            var client = new WebClient();

            var url = string.Format(BASEURL, amount, from.ToUpper(), to.ToUpper());

            // Get the result from the url's response as a string
            var response = client.DownloadString(url);

            // Create a regex to get the result from the response
            var pattern = new Regex("rhs: \"(\\d+.?\\d*)(.*)\"");
            var match = pattern.Match(response);

            // The first value is the numerical result
            string first = match.Groups[1].Value;

            // The second value is the resulting currency
            string second = match.Groups[2].Value.Substring(0, match.Groups[2].Value.Length - 10);

            // Return the concatenated result as a string
            return first + second;
        }

        /// <summary>
        ///     Converts a value from one currency to another, returning the value as a decimal number
        /// </summary>
        /// <param name="amount">The amount to convert</param>
        /// <param name="from">The currency code to convert from</param>
        /// <param name="to">The currency code to convert to</param>
        /// <returns>The numerical result as a decimal</returns>
        public static decimal Convert(decimal amount, string from, string to)
        {
            var client = new WebClient();

            var url = string.Format(BASEURL, amount, from.ToUpper(), to.ToUpper());

            // Get the result from the url's response as a string
            var response = client.DownloadString(url);

            // Create a regex to get the result from the response
            var pattern = new Regex("rhs: \\\"((\\d|\\s|\\.)*)(\\s[^\\s]+)");
            var match = pattern.Match(response);

            // The first value is the base numerical result
            string number = match.Groups[1].Value;

            // Sometimes the result number can have ASCII value 160 (from some reason) which would create an
            // error when converting to a decimal
            number = number.Replace(((char) 160).ToString(CultureInfo.InvariantCulture), "");

            // Convert the string numerical result into a decimal
            decimal num = System.Convert.ToDecimal(number);

            // Sometimes the result can have string multipliers such as "million"
            string units = match.Groups[3].Value.Replace(" ", "");

            // Multiply the number by the corresponding multiplier if present
            if (units.Equals("million"))
            {
                num *= 1000000;
            }
            else if (units.Equals("billion"))
            {
                num *= 1000000000;
            }
            else if (units.Equals("trillion"))
            {
                num *= 1000000000000;
            }

            // Return the numerical result rounded to two decimal places
            return Math.Round(num, 2);
        }
    }
}