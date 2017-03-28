using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace CondominioApp
{
    [Activity(Label = "CondominioApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;
        MediaPlayer mp,mp2;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            mp = MediaPlayer.Create(this, Resource.Raw.Bienvenido);
            mp2 = MediaPlayer.Create(this, Resource.Raw.ingrese);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            mp.Start();
            button.Click += delegate {

                mp2.Start();
                var activity2 = new Intent(this, typeof(LoginActivity));
                //activity2.PutExtra("MyData", "Data from Activity1");
                StartActivity(activity2);
            };
        }
    }
}

