using System;

namespace GoogleAPI
{
    /// <summary>
    ///     Tester class for the Currency utility methods
    ///     Ryan Harrison 2013
    ///     www.ryanharrison.co.uk
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Tester for the Currency class.
        ///     Prompts the user for two 3 letter currency codes and converts a value from one to the other
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            // Gain data from the user
            Console.WriteLine("Enter currency to convert from (3 letters) - ");
            var from = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter currency to convert to (3 letters) - ");
            var to = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter amount to convert - ");

            decimal amount;
            // Ensure that the user has given a numerical value to convert
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                try
                {
                    // Convert the currencies
                    Console.WriteLine(Currency.Convert(amount, from, to));
                }
                catch (Exception)
                {
                    // There was an error converting. Probably because the user entered invalid codes
                    Console.WriteLine("An error occurred whilst converting.");
                    Console.WriteLine("Please make sure you entered a valid currency, or try again later.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Amount must be numerical");
            }

            Console.ReadLine();
        }
    }
}