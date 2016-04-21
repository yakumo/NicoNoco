using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace NicoNoco.droid
{
    [Activity(Label = "NicoNoco.droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity // : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new NicoNocoApp.Common.NicoNocoApp ());
        }
    }
}

