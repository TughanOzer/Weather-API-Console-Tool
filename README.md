Weather-API Console Tool

C# console application for real-time weather data. This project was built to practice structured .NET development and API integration.

Uses async/await for API calls.
Maps nested JSON responses to C# classes.
Catches errors, like invalid city names or connection errors.
Loads API credentials from external JSON to keep keys secure and out of source control.

Built With:
- C# / .NET 10
- OpenWeatherMap API

To run:
1. You need your own API key by registering here: https://home.openweathermap.org
2. Rename 'api-keys.json.example' to 'api-keys.json' in the root directory
3. Replace 'YOUR_KEY_HERE' with your working openweathermap key.
4. Set api-keys.json properties to "Copy to Output Directory" in Visual Studio.
