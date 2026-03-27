using Ch10_App.Models;

namespace Ch10_App.Services;

public class WeatherDataService
{
    public IQueryable<Weather> GenerateWeatherData(int count = 1000)
    {
        var weatherList = new List<Weather>(count); 
        var rand = new Random(42);
        var baseDate = DateTime.Now; 

        for (int i = 1; i <= count; i++)
        {
            weatherList.Add(new Weather()
            {
                id = i,
                Date = baseDate.AddMinutes(i),
                TemperatureF = 72 + rand.Next(-2, 13) // 70-85°F
            });
        }

        return weatherList.AsQueryable();
    }
}
