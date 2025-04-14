using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppTempoAgora.Models;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        private static Tempo? t;

        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? tempo = null;

            string chave = "6135072afe7f6cec1537d5ccb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" + $"q={cidade}&units=metric&appid={chave}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObjet.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    T = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        Description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (string)rascunho["main"]["temp_min"],
                        temp_max = (string)rascunho["main"]["temp_max"],
                        speed = (string)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),

                    };

                }
            }

            return t;
        }
    }
}
