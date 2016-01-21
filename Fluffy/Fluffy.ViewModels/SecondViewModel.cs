namespace Fluffy.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Common;
    using Common.Args;
    using Common.Interfaces;

    public class SecondViewModel : MvxViewModel
    {
        private readonly IFluffyCommunicationService communicationService;
        private string updateString;

        public SecondViewModel(IFluffyCommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }


        public string UpdateString
        {
            get { return this.updateString; }
            set
            {
                this.updateString = value;
                this.RaisePropertyChanged(() => this.UpdateString);
            }
        }

        public void Init(SecondViewArgs viewArgs)
        {
            var s = $@"view init  -  First={viewArgs.First}, Second={viewArgs.Second}, Answer={viewArgs.Answer}";

            Debug.WriteLine(s);

            this.UpdateString = s;
        }

        public void ReloadState(SecondViewSavedSateArgs savedStateArgs)
        {
            var s = $@"Relaoded  -  First={savedStateArgs.First}, Second={savedStateArgs.Second}, Answer={savedStateArgs.Answer}";

            Debug.WriteLine(s);

            this.UpdateString = s;
        }

        public override async void Start()
        {
            var data = await this.communicationService.GetDataAsync().ConfigureAwait(false);
            if (data.IsSuccessful && data.Response !=null)
            {
                var u = data.Response.Users;
            }
        }

        public SecondViewSavedSateArgs SaveState()
        {
            return new SecondViewSavedSateArgs()
            {
                Answer = 2,
                Second = "Second",
                First = new Random().Next(0, 10).ToString()
            };
        }
    }
}
