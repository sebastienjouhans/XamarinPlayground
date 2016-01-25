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

            //var button = new UIButton(UIButtonType.RoundedRect);
            //button.SetTitle("Click me", UIControlState.Normal);
            //button.Frame = new Rectangle(10, 90, 300, 40);
            //this.Add(button);

            //var label = new UILabel(new Rectangle(10, 50, 300, 40));
            //this.Add(label);

            //var button2 = new UIButton(UIButtonType.RoundedRect);
            //button2.SetTitle("Navigate", UIControlState.Normal);
            //button2.Frame = new Rectangle(10, 90, 300, 40);
            //this.Add(button2);

            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(this.FeedbackText).To(vm => vm.UpdateString);
            set.Bind(this.MyButton).To(vm => vm.ItemClickCommand);
            set.Bind(this.Navigate).To(vm => vm.NavigateClickCommand).WithConversion("CommandParameter", "hello");
            set.Apply();
        }
    }
}