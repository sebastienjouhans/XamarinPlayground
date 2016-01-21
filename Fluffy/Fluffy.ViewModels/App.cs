namespace Fluffy.Core
{
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.IoC;
    using Cirrious.MvvmCross.ViewModels;
    using ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            this.InitialiseServices();
            this.InitialiseViewModels();
        }

        private void InitialiseServices()
        {
        }

        private void InitialiseViewModels()
        {
            Mvx.RegisterSingleton(new CustomAppStart());
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