namespace Fluffy.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Cirrious.MvvmCross.Droid.Views;

    [Activity(
    Label = "CustomBinding.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : MvxSplashScreenActivity
    {
        public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
        {
        }
    }
}