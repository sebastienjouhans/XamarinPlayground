namespace Fluffy.ViewModels
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Common.Interfaces;
    using Services.ActivityReporting;
    using Services.Communication;
    using Services.Serializers;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            this.InitialiseServices();

           this.RegisterAppStart(new CustomAppStart());
        }

        private void InitialiseServices()
        {
            Mvx.RegisterType<IJsonSerializer, JsonSerializer>();
            Mvx.RegisterType<IActivityReportingService, ActivityReportingService>();
            Mvx.RegisterType<IFluffyCommunicationService, FluffyCommunicationService>();
        }
    }

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly bool showFirstScreen;

        public CustomAppStart()
        {
            this.showFirstScreen = true;
        }

        public void Start(object hint = null)
        {
            if (this.showFirstScreen)
            {
                this.ShowViewModel<MainViewModel>();
            }
            else
            {
                this.ShowViewModel<SecondViewModel>();
            }
        }
    }
}