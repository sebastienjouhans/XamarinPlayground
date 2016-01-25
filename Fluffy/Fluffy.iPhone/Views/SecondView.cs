using System;

using UIKit;

namespace Fluffy.iPhone.Views
{
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Touch.Views;
    using ViewModels;

    public partial class SecondView : MvxViewController
    {
        public SecondView() : base("SecondView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var set = this.CreateBindingSet<SecondView, SecondViewModel>();
            set.Bind(this.InitVariables).To(vm => vm.UpdateString);
            set.Apply();
        }
    }
}