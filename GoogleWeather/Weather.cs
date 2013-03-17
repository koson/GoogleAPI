using System;
using System.Collections.Generic;
using System.Xml;

namespace GoogleWeather
{
    public class Weather
    {
        public static WeatherCondition GetCurrentConditions(string city)
        {
            var cond = new WeatherCondition();
            var doc = new XmlDocument();
            XmlTextReader reader = null;

            try
            {
                reader = new XmlTextReader(string.Format("http://www.google.com/ig/api?weather={0}&referrer=googlecalendar", city));
                doc.Load(reader);

                if (doc.SelectSingleNode("xml_api_reply/weather/problem_cause") != null)
                {
                    cond = null;
                }
                else
                {
                    cond.City = doc.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
                    cond.Condition = doc.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition").Attributes["data"].InnerText;
                    cond.TempC = doc.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_c").Attributes["data"].InnerText;
                    cond.TempF = doc.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_f").Attributes["data"].InnerText;
                    cond.Humidity = doc.SelectSingleNode("/xml_api_reply/weather/current_conditions/humidity").Attributes["data"].InnerText;
                    cond.Wind = doc.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition").Attributes["data"].InnerText;
                }
            }
            catch (Exception)
            {
                cond = null;

            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return cond;
        }

        public static List<WeatherCondition> GetForecastConditions(string city)
        {
            var doc = new XmlDocument();
            XmlTextReader reader = null;
            var conditions = new List<WeatherCondition>();

            try
            {
                reader = new XmlTextReader(string.Format("http://www.google.com/ig/api?weather={0}&referrer=googlecalendar", city));
                doc.Load(reader);

                if (doc.SelectSingleNode("xml_api_reply/weather/problem_cause") != null)
                {
                    conditions = null;
                }
                else
                {
                    foreach (XmlNode node in doc.SelectNodes("/xml_api_reply/weather/forecast_conditions"))
                    {
                        var cond = new WeatherCondition();
                        cond.City = doc.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
                        cond.Condition = node.SelectSingleNode("condition").Attributes["data"].InnerText;
                        cond.High = node.SelectSingleNode("high").Attributes["data"].InnerText;
                        cond.Low = node.SelectSingleNode("low").Attributes["data"].InnerText;
                        cond.Day = node.SelectSingleNode("day_of_week").Attributes["data"].InnerText;
                        conditions.Add(cond);
                    }
                }
            }
            catch (Exception)
            {
                conditions = null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return conditions;
        }
    }
}
