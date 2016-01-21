namespace Fluffy.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Cirrious.MvvmCross.Droid.Views;
    using ViewModels;

    [Activity(
        Label = "Fluffy", 
        MainLauncher = true, 
        Icon = "@drawable/icon", 
        NoHistory = true, 
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainView : MvxActivity
    {
        public new MainViewModel ViewModel
        {
            get { return (MainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.Main);
        }
    }
}


