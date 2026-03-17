using System.Text.Json.Serialization;

public sealed class ForecastResponse
{
    [JsonPropertyName("list")]
    public List<ForecastItem> List { get; set; } = [];  
}

public sealed class ForecastItem
{
    [JsonPropertyName("dt")]
    public long Dt { get; set; }

    [JsonPropertyName("main")]
    public MainInfo? Main { get; set; } 

    [JsonPropertyName("weather")]
    public List<WeatherInfo> Weather { get; set; } = [];
}

public sealed class MainInfo
{
    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }    

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }     
}

public sealed class WeatherInfo
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }  

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }  
}
