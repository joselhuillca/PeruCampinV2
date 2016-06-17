using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;
using Android.Views;
using Android.Util;
using System;



namespace MLearning.Droid
{
    [Activity(
		Label = "Perú Camping"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {

			//this.Window.AddFlags(WindowManagerFlags.Fullscreen);

        }


    }
}