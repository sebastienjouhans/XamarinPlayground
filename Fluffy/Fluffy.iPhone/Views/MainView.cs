using System;

using UIKit;

namespace Fluffy.iPhone.Views
{
    using System.Drawing;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Touch.Views;
    using ViewModels;

    public partial class MainView : MvxViewController
    {
        public MainView() : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(this.FeedbackText).To(vm => vm.UpdateString);
            set.Bind(this.MyButton).To(vm => vm.ItemClickCommand);
            set.Bind(this.Navigate).To(vm => vm.NavigateClickCommand).WithConversion("CommandParameter", "hello");
            set.Apply();
        }
    }
}