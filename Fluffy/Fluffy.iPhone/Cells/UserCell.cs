namespace Fluffy.iPhone.Cells
{
    using System;
    using System.Windows.Input;
    using Cirrious.MvvmCross.Binding.Touch.Views;
    using Foundation;


    /// <summary>
    /// good example 
    /// https://github.com/MvvmCross/MvvmCross-Tutorials/blob/6f76d41c8f5b3555f3ed5d76f0c0fd8d2c929a79/Sample%20-%20CirriousConference/Cirrious.Conference.UI.Touch/Cells/TweetCell3.cs
    /// </summary>
    public partial class UserCell
            : MvxTableViewCell
    {
        public static NSString Identifier = new NSString("UserCell");
        public const string BindingText = @"
SelectedCommand Command;
ImageUrl ThumbImage;
NameText Name;
AgeText Age;
";

		private MvxImageViewWrapper imageWrapper;
                
        public UserCell(IntPtr handle)
            : base(BindingText, handle)
        {
            this.Initialise();
        }		
        
        public UserCell()
            : base(BindingText)
        {
            this.Initialise();
        }
		        
		private void Initialise ()
		{
			this.imageWrapper = new MvxImageViewWrapper(() => this.ThumbImage);
		}

        protected override void Dispose (bool disposing)
        {
            if (disposing)
            {
				this.imageWrapper.Dispose();

                // TODO - really not sure that Dispose is the right place for this call 
                // - but couldn't see how else to do this in a TableViewCell
                this.ReleaseDesignerOutlets();
            }
            
            base.Dispose (disposing);
        } 

        public override NSString ReuseIdentifier => Identifier;

        public string ImageUrl
		{
			get { return this.imageWrapper.ImageUrl; }
			set { this.imageWrapper.ImageUrl = value; }
		}
		
		public string NameText
        {
            get { return this.NameLabel.Text; }
            set { this.NameLabel.Text = value; }
        }
        
        public string AgeText
        {
            get { return this.AgeLabel.Text; }
            set { this.AgeLabel.Text = value; this.AgeLabel.SizeToFit(); }
        }

		public ICommand SelectedCommand {get;set;}

		bool _isSelected;
		public override void SetSelected (bool selected, bool animated)
		{
			base.SetSelected (selected, animated);

			if (this._isSelected == selected) 
			{
				return;
			}

			this._isSelected = selected;
			if (!this._isSelected) 
			{
				return;
			}

			if (this.SelectedCommand != null)
			{
				this.SelectedCommand.Execute(this);
			}
		}
    }
}

