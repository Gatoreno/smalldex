﻿using Newtonsoft.Json;
using smallDex.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smallDex.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokeInfo : ContentPage
    {
        public string urlpx;
        public PokeInfo(string url)
        {
            InitializeComponent();
            urlpx = url;
            _ = GetPokesFromWeb();
        }

        public async Task GetPokesFromWeb()
        {

            try
            {
                HttpClient client = new HttpClient();


                var uri = urlpx;



                var responseMsg = await client.GetAsync(uri);



                switch (responseMsg.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");

                        //xlabel.Text = "error 500";
                        // Cator.IsVisible = false;
                        break;


                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");

                        // ylabel.Text = "Ultimas noticas de proyectos";


                        // var json_ = JsonConvert.DeserializeObject<List<VisitasMod>>(xjson);

                        string xjson = await responseMsg.Content.ReadAsStringAsync();
                        //DireccApiCall

                        HttpContent contentD = responseMsg.Content;
                        var xjsonD = await contentD.ReadAsStringAsync();



                        var json_d = JsonConvert.DeserializeObject<SiglePokeInfo>(xjsonD);
                        pokename.Text = json_d.name;
                        img.Source = json_d.sprites.front_default;




                        break;

                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                //  Cator.IsVisible = false;

                //  CatorT.Text = "Ha habido un error";
                return;
            }
        }
    }
}