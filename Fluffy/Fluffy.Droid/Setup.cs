namespace Fluffy.Droid
{
    using Android.Content;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;
    using Core;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}