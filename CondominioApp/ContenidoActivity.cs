using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace CondominioApp
{
    [Activity(Label = "ContenidoActivity")]
    public class ContenidoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Contenido);
            Button button = FindViewById<Button>(Resource.Id.getWeatherButton);
            Button button2 = FindViewById<Button>(Resource.Id.getWeatherButton2);
            Button button3 = FindViewById<Button>(Resource.Id.getWeatherButton3);
            Button button4 = FindViewById<Button>(Resource.Id.getWeatherButton4);
            Button button5 = FindViewById<Button>(Resource.Id.btnMedioPago);
            var textView1 = FindViewById<TextView>(Resource.Id.textView1);
            var textView2 = FindViewById<TextView>(Resource.Id.textView2);
            var textView3 = FindViewById<TextView>(Resource.Id.textView3);
            var textView4 = FindViewById<TextView>(Resource.Id.textView4);
            string usuarioLogin = Intent.GetStringExtra("usuarioDesdeLogin") ?? "Data not available";
            string condominioLogin = Intent.GetStringExtra("condominioDesdeLogin") ?? "Data not available";
            button5.Click += delegate
            {
                var activityMedio = new Intent(this, typeof(MedioPagoActivity));
                //activity2.PutExtra("MyData", "Data from Activity1");
                StartActivity(activityMedio);
            };
                button.Click += async (sender, e) =>
            {
                button.Enabled = false;
                string url = "https://api.myjson.com/bins/2t2db";
                JsonValue json = await FetchWeatherAsync(url);
                string validacion = json.ToString();
                bool b = validacion.Contains("espaciosComunes");

                //if (b == false )
                //{
                //  Toast.MakeText(this, "No hay datos", ToastLength.Long).Show();
                //   button.Enabled = true;
                //   return;

                // else
                string aux = "";
                string espacios = "";
                //string weatherResults = json["espaciosComunes"]["condominio"][condominioLogin]["Quincho"].ToString();
                //string weatherResults2 = json["espaciosComunes"]["condominio"][condominioLogin]["Sala Multiuso"].ToString();

                /*string resultado1 = "";
                    string resultado2 = "";
                    if (weatherResults == "0")
                    {
                        resultado1 = "ocupado";
                    }
                    else
                        resultado1 = "Disponible";
                    if (weatherResults2 == "0")
                    {
                        resultado2 = "ocupado";
                    }
                    else
                        resultado2 = "Disponible";
                        */
                string data = json["espaciosComunes"]["condominio"][condominioLogin].ToString();
                textView1.Text = data.Replace("{", "").Replace("}", "").Replace("0", "Disponible").Replace("1", "Ocupado");
                
                    button.Enabled = true;
                
            };
            button2.Click += async (sender, e) =>
            {
                button2.Enabled = false;
                string url = "https://api.myjson.com/bins/2t2db";
                JsonValue json = await FetchWeatherAsync(url);
                string validacion = json.ToString();
                
                bool b = validacion.Contains("estacionamientos");

                //if (b == false)
               // {
               //     Toast.MakeText(this, "No hay datos", ToastLength.Long).Show();
               //     button.Enabled = true;
               //     return;
               // }
                //else
                
                    string weatherResults = json["estacionamientos"]["condominio"][condominioLogin]["Estacionamiento 1"].ToString();
                    //string weatherResults2 = json["estacionamientos"]["condominio"][condominioLogin]["Estacionamiento 2"].ToString();
                  //  string weatherResults3 = json["estacionamientos"]["condominio"][condominioLogin]["Estacionamiento 3"].ToString();
                    string resultado1 = "";
                    string resultado2 = "";
                    string resultado3 = "";

                    if (weatherResults == "1")
                    {
                        resultado1 = "ocupado";
                    }
                    else
                        resultado1 = "Disponible";
                /* if (weatherResults2 == "1")
                 {
                     resultado2 = "ocupado";
                 }
                 else
                     resultado2 = "Disponible";
                 if (weatherResults3 == "1")
                 {
                     resultado3 = "ocupado";
                 }
                 else
                     resultado3 = "Disponible";
                 */

                //textView2.Text = weatherResults;
                string data = json["estacionamientos"]["condominio"][condominioLogin].ToString();
                textView2.Text = data.Replace("{", "").Replace("}", "").Replace("0", "Disponible").Replace("1", "Ocupado");
                //textView2.Text = " Estacionamiento 1 : " + resultado1 + "\r\n" + " Estacionamiento 2 : " + resultado2 + "\r\n" + " Estacionamiento 3 : " + resultado3;
                    button2.Enabled = true;
                
            };
            button3.Click += async (sender, e) =>
            {
                button3.Enabled = false;
                string url = "https://api.myjson.com/bins/2t2db";
                JsonValue json = await FetchWeatherAsync(url);
                string validacion = json.ToString();
                bool b = validacion.Contains("gastosComunes");

                if (b == true)
                {
                    string weatherResults = json["gastosComunes"][usuarioLogin].ToString();
                    textView3.Text = weatherResults;
                    button3.Enabled = true;
                }
                else
                {
      
                    Toast.MakeText(this, "No hay datos", ToastLength.Long).Show();
                    button.Enabled = true;
                    return;

                }
            };

            button4.Click += async (sender, e) =>
            {
                button4.Enabled = false;
                string url = "https://api.myjson.com/bins/2t2db";
                JsonValue json = await FetchWeatherAsync(url);

                string validacion = json.ToString();
                bool b = validacion.Contains("morosidad");

                if (b == false)
                {
                    Toast.MakeText(this, "No hay datos", ToastLength.Long).Show();
                    button.Enabled = true;
                    return;
                }
                else
                {
                    string weatherResults = json["morosidad"][usuarioLogin].ToString();
                    textView4.Text = weatherResults;
                    button4.Enabled = true;
                }
            };
        }

        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

    }
}