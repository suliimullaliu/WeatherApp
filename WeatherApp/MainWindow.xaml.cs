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
        private readonly String apiKey = "8e879e34ce13e3d617a82f50b3190755";

        private String requestUrl = "https://api.openweathermap.org/data/2.5/weather";
        public MainWindow()
        {
            InitializeComponent();
            HttpClient httpClient = new HttpClient();

            var stadt = "Zschopau";
            var finalUri = requestUrl + "?q="+stadt+"&appid=" + apiKey+"&units=metric";

            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;

            String response = httpResponse.Content.ReadAsStringAsync().Result;

            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            Console.WriteLine(response);

            

        }
    }
}
