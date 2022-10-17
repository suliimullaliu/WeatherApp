using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WeatherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "8e879e34ce13e3d617a82f50b3190755";

        private string requestUrl = "https://api.openweathermap.org/data/2.5/weather";
        private string finalImage;

        public MainWindow()
        {
            InitializeComponent();

            UpdateUI("Zschopau");
        }

        public void UpdateUI(String City)
        {
            string finalImage = "sun.png";

            WeatherMapResponse Result = GetWeatherData(City);
            string currentWeather = Result.weather[0].main.ToLower();


            if (currentWeather.Contains("cloud"))
            {
                finalImage = "Cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "Rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "Snow.png";
            }

            imageBackground.ImageSource = new BitmapImage(new Uri("images/" + finalImage, UriKind.Relative));


            labelTemperature.Content = Result.main.temp.ToString("F0") + "°C";
            labelInfo.Content = Result.weather[0].main;
        }


        public WeatherMapResponse GetWeatherData(String city)
        {

            HttpClient httpClient = new HttpClient();

           
            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric";

            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;

            String response = httpResponse.Content.ReadAsStringAsync().Result;

            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            return weatherMapResponse;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = TextBoxQuery.Text;
            UpdateUI(query);
        }

    }

    
}
