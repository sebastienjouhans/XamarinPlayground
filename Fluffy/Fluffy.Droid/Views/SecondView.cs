namespace Fluffy.Droid.Views
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Graphics;
    using Android.Views;
    using Android.Widget;
    using Cirrious.MvvmCross.Binding.Droid.BindingContext;
    using Cirrious.MvvmCross.Binding.Droid.Views;
    using Cirrious.MvvmCross.Droid.Views;
    using Common;
    using Common.Entities;
    using ViewModels;

    [Activity(
        Label = "Fluffy 2",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SecondView : MvxActivity
    {
        public new SecondViewModel ViewModel
        {
            get { return (SecondViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.Second);

            var list = this.FindViewById<ListView>(Resource.Id.TradeConfirmation);
            list.Adapter = new CustomAdapter(this, (IMvxAndroidBindingContext) this.BindingContext);
        }
    }

    public class CustomAdapter : MvxAdapter
    {
        public CustomAdapter(Context context) : base(context)
        {
        }

        public CustomAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context, bindingContext)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var v = base.GetView(position, convertView, parent);
            v.SetBackgroundColor(position % 2 == 0 ? Color.Red : Color.Blue);
            return v;
        }

        protected override View GetBindableView(View convertView, object source, int templateId)
        {
            if (source is User)
                templateId = Resource.Layout.seb_item_listview;

            return base.GetBindableView(convertView, source, templateId);
        }
    }
}


