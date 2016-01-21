namespace Fluffy.Core
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.ViewModels;
    using Common.Interfaces;
    using Services.Serializers;
    using ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            this.InitialiseServices();

            Mvx.RegisterSingleton(new CustomAppStart());
        }

        private void InitialiseServices()
        {
            Mvx.RegisterType<IJsonSerializer, JsonSerializer>();
            Mvx.RegisterType<ICommunicationService, IFluffyCommunicationService>();
        }

    }

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly bool service;

        public CustomAppStart()
        {
            this.service = false;
        }

        public void Start(object hint = null)
        {
            if (this.service)
            {
                this.ShowViewModel<SecondViewModel>();
            }
            else
            {
                this.ShowViewModel<SecondViewModel>();
            }
        }
    }
}