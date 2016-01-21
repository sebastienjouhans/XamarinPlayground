namespace Fluffy.Droid.Views
{
    using Android.App;
    using Android.Content.PM;
    using Cirrious.MvvmCross.Droid.Views;
    using Common;
    using ViewModels;

    [Activity(
        Label = "Fluffy 2",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SecondView : MvxActivity
    {
        public new SecondViewModel ViewModel
        {
            get { return (SecondViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.Page2);
        }

        protected override void OnResume()
        {
            base.OnResume(); // Always call the superclass first.

        }

        protected override void OnPause()
        {
            base.OnPause(); // Always call the superclass first

        }
    }
}


