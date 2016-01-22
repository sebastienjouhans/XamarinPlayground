namespace Fluffy.Droid
{
    using Android.Content;
    using Cirrious.CrossCore.Plugins;
    using Cirrious.MvvmCross.Plugins.Visibility;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;
    using ViewModels;

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