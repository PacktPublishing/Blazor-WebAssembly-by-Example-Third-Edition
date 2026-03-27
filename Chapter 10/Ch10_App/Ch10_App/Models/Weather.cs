namespace Ch10_App.Models
{
    public class Weather
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureF { get; set; }
        public int TemperatureC => (int)Math.Round((TemperatureF - 32) / 1.8);
        public string TemperatureCategory => TemperatureF > 90 ? "Hot" : "";
    }
}
