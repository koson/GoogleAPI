# Google API's

## Google Currency

A small utility class using the Google Currency API to convert from one currency to another.

Convert methods either return a decimal or a string (e.g "23.54 U.S Dollars")

A tester class is included to show an example usage of the Currency class.

Unfortunately the Google Currency API doesn't seem to give normalised results.
The resulting JSON object sometimes includes a simple numerical result (e.g .45), but sometimes includes string multiplers such as "12 million" or random ASCII character such as ASCII 160.

---------

## Google Weather

A small utility class using the Google Weather API to display the current and forecast weather conditions for a city.

A tester class is included to show an example usage of the Weather class.