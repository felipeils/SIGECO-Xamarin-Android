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
using Android.Text;
using Android.Media;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace CondominioApp
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        MediaPlayer mp;
        string usuario = "";
        string usuarioGuardado = "";
        string contrasena = "";
        string condominio = "";
        
        private async void validar()
        {
           

            string url = "https://api.myjson.com/bins/2t2db";
            JsonValue json = await FetchWeatherAsync(url);
            string weatherResults = json["users"].ToString();
            EditText txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            usuario = weatherResults;
            
        }

        private async void validar2()
        {
            string url = "https://api.myjson.com/bins/2t2db";
            JsonValue json = await FetchWeatherAsync(url);
            string weatherResults = json["users"][usuarioGuardado]["clave"].ToString();
            string weatherResults2 = json["users"][usuarioGuardado]["condominio"].ToString();
            EditText txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            contrasena = weatherResults;
            condominio = weatherResults2;

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            validar();
            mp = MediaPlayer.Create(this, Resource.Raw.favor);
            Button button = FindViewById<Button>(Resource.Id.btnIngresarLogin);
            EditText txtUsuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            EditText txtContrasena = FindViewById<EditText>(Resource.Id.txtContrasena);

            button.Click += delegate
            {
                
                string strContrasena = txtContrasena.Text.ToString();
                string strUsuario = txtUsuario.Text.ToString();
               
                bool b = usuario.Contains(strUsuario);
                bool c = contrasena.Contains(strContrasena);



                if (strUsuario.Length < 5 || strContrasena.Length < 5)
                {
                    mp.Start();
                    Toast.MakeText(this, "Ingrese usuario o contraseña correcta", ToastLength.Long).Show();
                    return;
                }
                else
                {
                    validar();
                    if (b)
                    {
                        usuarioGuardado = strUsuario;
                        validar2();
                    }

                    if (b == true && c == true)
                    {

                        var activity3 = new Intent(this, typeof(ContenidoActivity));
                        activity3.PutExtra("usuarioDesdeLogin", usuarioGuardado);
                        activity3.PutExtra("condominioDesdeLogin", condominio);
                        Toast.MakeText(this, "Ingreso logrado", ToastLength.Long).Show();
                        StartActivity(activity3);
                    }
                    else
                        //Toast.MakeText(this, "Ingrese usuario o contraseña correcta", ToastLength.Long).Show();

                    return;
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
                using (System.IO.Stream stream = response.GetResponseStream())
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