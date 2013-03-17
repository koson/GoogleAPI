using System;
using System.Collections.Generic;

namespace GoogleWeather
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var option = 0;

            PrintMenu();

            while (option != 3)
            {
                if (GetInputChoice(out option))
                {
                    string city;
                    switch (option)
                    {
                        case 1: //Current
                            {
                                city = GetCity();
                                var conditions = Weather.GetCurrentConditions(city);
                                if (conditions != null)
                                {
                                    PrintCurrentForecast(conditions);
                                }
                                else
                                {
                                    PrintErrorMessage();
                                }
                                break;
                            }
                        case 2: //Forecast
                            {
                                city = GetCity();
                                var foreacast = Weather.GetForecastConditions(city);
                                if (foreacast != null && foreacast.Count > 1)
                                {
                                    PrintForecastConditions(foreacast);
                                }
                                else
                                {
                                    PrintErrorMessage();
                                }
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                Console.WriteLine();
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("Input must be numeric");
                    Console.WriteLine();
                }
            }

        }

        private static void PrintMenu()
        {
            Console.WriteLine("1 - Current Conditions");
            Console.WriteLine("2 - Forecast Conditions");
            Console.WriteLine("3 - Exit");
            Console.WriteLine();
        }

        private static void PrintErrorMessage()
        {
            Console.WriteLine("There was an error processing the request.");
            Console.WriteLine("Please, make sure you are using a valid city, or try again later.");
            Console.WriteLine();
        }

        private static bool GetInputChoice(out int op)
        {
            Console.WriteLine("Enter an option - ");
            var input = Console.ReadLine();

            return int.TryParse(input, out op);
        }

        private static string GetCity()
        {
            Console.WriteLine();
            Console.WriteLine("Enter a city - ");
            return Console.ReadLine();
        }

        private static void PrintCurrentForecast(WeatherCondition conditions)
        {
            Console.WriteLine("CurrentConditions for: " + conditions.City);
            Console.WriteLine("Conditions: " + conditions.Condition);
            Console.WriteLine("Temperature (F): " + conditions.TempF);
            Console.WriteLine("Temperature (C): " + conditions.TempC);
            Console.WriteLine("Humidity: " + conditions.Humidity);
            Console.WriteLine("Wind: " + conditions.Wind);
            Console.WriteLine();
        }

        private static void PrintForecastConditions(List<WeatherCondition> conditions)
        {
            Console.WriteLine("Foreacast Conditions for: " + conditions[0].City);

            foreach (var c in conditions)
            {
                Console.WriteLine("Day: " + c.Day);
                Console.WriteLine("Conditions: " + c.Condition);
                Console.WriteLine("Temperature (High): " + c.High);
                Console.WriteLine("Temperature (Low): " + c.Low);
                Console.WriteLine();
            }
        }
    }
}
