namespace Fluffy.Droid
{
    using Android.Content;
    using Cirrious.CrossCore.Plugins;
    using Cirrious.MvvmCross.Plugins.Visibility;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.ViewModels;
    using Core;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();

            base.LoadPlugins(pluginManager);
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}