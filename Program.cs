using System.Linq.Expressions;
using System.Text.Json;

string keyJson = File.ReadAllText("api-keys.json");
ApiKeys apiData = ParseApiKeys(keyJson);

using HttpClient client = new HttpClient();
bool isRunning = true;

while (isRunning) {
    try {
        string city = GetUserCity();
        if (city.ToLower() == "exit") {
            isRunning = false;
            continue;
        }
        string rawJson = await FetchWeatherDataAsync(city, apiData.ApiKey, client);
        WeatherData weatherData = ParseWeatherData(rawJson);
        DisplayWeather(weatherData);
    }
    catch (HttpRequestException) {
        Console.WriteLine("Error: This city does not exist or the network is down. Please try again.");
    }
    catch (Exception unknown) {
        Console.WriteLine($"Unkown error: {unknown.Message}");
    }
    Console.WriteLine("\n Type 'exit' to quit the application, or enter another city \n");
}

string GetUserCity() {
    Console.WriteLine("To show the weather, please enter the city name:");
    return Console.ReadLine() ?? "";
}

async Task<string> FetchWeatherDataAsync(string cityName, string appId, HttpClient client) {
    Console.WriteLine($"Loading weather data for {cityName}...");
    return await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={appId}&units=metric");
    //Note Learning: Using HttpClient client (automatic) or client.Dispose(); (manual)
}

WeatherData ParseWeatherData(string rawJsonresponse) {
    WeatherData? weatherData = JsonSerializer.Deserialize<WeatherData>(rawJsonresponse);
    if (weatherData != null) {
        return weatherData;
    }
    throw new Exception("Failed to parse weather data.");
}

ApiKeys ParseApiKeys(string jsonContent) {
    ApiKeys? ApiKey = JsonSerializer.Deserialize<ApiKeys>(jsonContent);
    if (ApiKey != null) {
        return ApiKey;
    }
    throw new Exception("Failed to parse api-keys.json");
}

void DisplayWeather(WeatherData weatherData) {
    Console.WriteLine($"Weather in {weatherData.name}, {weatherData.sys?.country} is {weatherData.main?.temp}°C and a humidity of {weatherData.main?.humidity}%");
}