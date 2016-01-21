namespace Fluffy.ViewModels
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;
    using Common;

    public class MainViewModel : MvxViewModel
    {
        private double counter;
        private MvxCommand itemClickCommand;
        private string updateString;
        private MvxCommand<string> navigateClickCommand;

        public MainViewModel()
        {
            
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = this.itemClickCommand ?? new MvxCommand(this.CountUp);
                return this.itemClickCommand;
            }
        }


        public ICommand NavigateClickCommand
        {
            get
            {
                this.navigateClickCommand = this.navigateClickCommand ?? new MvxCommand<string>(this.Navigate);
                return this.navigateClickCommand;
            }
        }

        private void Navigate(string s)
        {
            this.ShowViewModel<SecondViewModel>( new SecondViewArgs { First = s, Second = "Another", Answer = 1});
        }

        private void CountUp()
        {
            this.counter++;

            this.UpdateString = $@"Click count = {this.counter}";
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
    }
}
