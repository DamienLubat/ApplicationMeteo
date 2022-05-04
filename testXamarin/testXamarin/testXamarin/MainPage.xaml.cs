using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Essentials;
using System.Diagnostics;

namespace testXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /*Fonction pour obtion saisie code postal*/
        private void fonctswi(object sender, ToggledEventArgs e)
        {
            if (switc.IsToggled)
            {

                cp.IsVisible = true;
                foss.IsVisible = true;
                sarc.IsVisible = true;
                jagn.IsVisible = true;
                loca.IsVisible = false;


            }
            else
            {

                cp.IsVisible = false;
                foss.IsVisible = false;
                sarc.IsVisible = false;
                jagn.IsVisible = false;
                loca.IsVisible = true;


            }
        }

        /*Saisie par ville*/
        private void Button_Clicked(object sender, EventArgs e)
        {
            getMeteo();
        }
        async void getMeteo()

        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                    string request = $"?q={cp.Text},fr&appid=11ac137ae63ba854fec5a1626f18426b&units=metric&lang=fr";

                    string jsonString = await client.GetStringAsync(request);
                    //label1.Text = jsonString;
                    var meteo = OpenWeatherMap.FromJson(jsonString);
                    //label1.Text = meteo.Main.Temp.ToString()+"°C";
                    temp.Text = meteo.Main.Temp.ToString() + "°C";
                    vent.Text = meteo.Wind.Speed.ToString() + "km/m";
                    humidité.Text = meteo.Main.Humidity.ToString() + "%";
                    visibilité.Text = meteo.Weather[0].Description;
                    lever.Text = UnixTimeStampToDateTime(meteo.Sys.Sunrise);
                    coucher.Text = UnixTimeStampToDateTime(meteo.Sys.Sunset);
                    img.Source = $"https://openweathermap.org/img/w/{meteo.Weather[0].Icon}.png";
                    //Meteo.Source = $"https://tile.openweathermap.org/map/clouds_new/2/3/3.png?appid=11ac137ae63ba854fec5a1626f18426b";
                    ville.Text = meteo.Name;
                    result.IsVisible = true;
                    ville.IsVisible = true;
                    /*Localisation*/
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }

        }

        /*C*/
        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtdateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtdateTime = dtdateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            var heure = dtdateTime.ToShortTimeString();
            return heure;
        }


        /*Par Localisation*/
        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                {
                    LabelLocation.Text = "Pas de GPS";
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                        client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                        string request = $"?lat={location.Latitude}&lon={location.Longitude}&appid=11ac137ae63ba854fec5a1626f18426b&units=metric&lang=fr";
                        string jsonString = await client.GetStringAsync(request);
                        var meteo = OpenWeatherMap.FromJson(jsonString);
                        //label1.Text = meteo.Main.Temp.ToString()+"°C";
                        temp.Text = meteo.Main.Temp.ToString() + "°C";
                        vent.Text = meteo.Wind.Speed.ToString() + "km/m";
                        humidité.Text = meteo.Main.Humidity.ToString() + "%";
                        visibilité.Text = meteo.Weather[0].Description;
                        lever.Text = UnixTimeStampToDateTime(meteo.Sys.Sunrise);
                        coucher.Text = UnixTimeStampToDateTime(meteo.Sys.Sunset);
                        img.Source = $"https://openweathermap.org/img/w/{meteo.Weather[0].Icon}.png";
                        //Meteo.Source = $"https://tile.openweathermap.org/map/clouds_new/2/3/3.png?appid=11ac137ae63ba854fec5a1626f18426b";
                        ville.Text = meteo.Name;
                        result.IsVisible = true;
                        ville.IsVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }
        /*Meteo a Fosses*/
        async private void Button_Clicked_Fosses(object sender, EventArgs e)
        {
            try
            {
                
                using (HttpClient client = new HttpClient()){
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                    string request = $"?lat=49.0990&lon=2.5016&appid=11ac137ae63ba854fec5a1626f18426b&units=metric&lang=fr";
                    string jsonString = await client.GetStringAsync(request);
                    var meteo = OpenWeatherMap.FromJson(jsonString);
                    //label1.Text = meteo.Main.Temp.ToString()+"°C";
                    temp.Text = meteo.Main.Temp.ToString() + "°C";
                    vent.Text = meteo.Wind.Speed.ToString() + "km/m";
                    humidité.Text = meteo.Main.Humidity.ToString() + "%";
                    visibilité.Text = meteo.Weather[0].Description;
                    lever.Text = UnixTimeStampToDateTime(meteo.Sys.Sunrise);
                    coucher.Text = UnixTimeStampToDateTime(meteo.Sys.Sunset);
                    img.Source = $"https://openweathermap.org/img/w/{meteo.Weather[0].Icon}.png";
                    //Meteo.Source = $"https://tile.openweathermap.org/map/clouds_new/2/3/3.png?appid=11ac137ae63ba854fec5a1626f18426b";
                    ville.Text = meteo.Name;
                    result.IsVisible = true;
                    ville.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }

        /*Meteo a Sarcelles*/
        async private void Button_Clicked_Sarcelles(object sender, EventArgs e)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                    string request = $"?lat=48.9930&lon=2.3829&appid=11ac137ae63ba854fec5a1626f18426b&units=metric&lang=fr";
                    string jsonString = await client.GetStringAsync(request);
                    var meteo = OpenWeatherMap.FromJson(jsonString);
                    //label1.Text = meteo.Main.Temp.ToString()+"°C";
                    temp.Text = meteo.Main.Temp.ToString() + "°C";
                    vent.Text = meteo.Wind.Speed.ToString() + "km/m";
                    humidité.Text = meteo.Main.Humidity.ToString() + "%";
                    visibilité.Text = meteo.Weather[0].Description;
                    lever.Text = UnixTimeStampToDateTime(meteo.Sys.Sunrise);
                    coucher.Text = UnixTimeStampToDateTime(meteo.Sys.Sunset);
                    img.Source = $"https://openweathermap.org/img/w/{meteo.Weather[0].Icon}.png";
                    //Meteo.Source = $"https://tile.openweathermap.org/map/clouds_new/2/3/3.png?appid=11ac137ae63ba854fec5a1626f18426b";
                    ville.Text = meteo.Name;
                    result.IsVisible = true;
                    ville.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }

        /*Meteo a Jagny-sous-Bois*/
        async private void Button_Clicked_Jagny(object sender, EventArgs e)
        {
            try{
                using (HttpClient client = new HttpClient())
                    {
                        //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                        client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                        string request = $"?q=Jagny-sous-Bois&appid=11ac137ae63ba854fec5a1626f18426b&units=metric&lang=fr";
                        string jsonString = await client.GetStringAsync(request);
                        var meteo = OpenWeatherMap.FromJson(jsonString);
                        //label1.Text = meteo.Main.Temp.ToString()+"°C";
                        temp.Text = meteo.Main.Temp.ToString() + "°C";
                        vent.Text = meteo.Wind.Speed.ToString() + "km/m";
                        humidité.Text = meteo.Main.Humidity.ToString() + "%";
                        visibilité.Text = meteo.Weather[0].Description;
                        lever.Text = UnixTimeStampToDateTime(meteo.Sys.Sunrise);
                        coucher.Text = UnixTimeStampToDateTime(meteo.Sys.Sunset);
                        img.Source = $"https://openweathermap.org/img/w/{meteo.Weather[0].Icon}.png";
                        //Meteo.Source = $"https://tile.openweathermap.org/map/precipitation_new/2/3/3.png?appid=11ac137ae63ba854fec5a1626f18426b";
                        ville.Text = meteo.Name;
                        result.IsVisible = true;
                        ville.IsVisible = true;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }
    }
}