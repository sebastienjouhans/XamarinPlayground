namespace Fluffy.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Common;
    using Common.Args;
    using Common.Entities;
    using Common.Interfaces;

    public class SecondViewModel : MvxViewModel
    {
        private readonly IFluffyCommunicationService communicationService;
        private string updateString;
        private ObservableCollection<User> users;

        private MvxCommand<User> userItemClickCommand;
        private bool isLoading;

        public SecondViewModel(IFluffyCommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }

        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                if (!Equals(value, this.users))
                {
                    this.users = value;
                    this.RaisePropertyChanged(() => this.Users);
                }
            }
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

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
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
            this.IsLoading = true;
            var data = await this.communicationService.GetDataAsync().ConfigureAwait(false);
            if (data.IsSuccessful && data.Response !=null)
            {
                this.Users = new ObservableCollection<User>(data.Response.Users);
            }
            this.IsLoading = false;
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

        private void UserItemClick(User user)
        {

        }
    }
}
