// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

namespace Fluffy.iPhone.Cells
{
    using System.CodeDom.Compiler;
    using Foundation;
    using UIKit;

    [Register ("UserCell")]
	partial class UserCell
    {
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel AgeLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ThumbImage { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (this.AgeLabel != null) {
				this.AgeLabel.Dispose ();
				this.AgeLabel = null;
			}
			if (this.NameLabel != null) {
				this.NameLabel.Dispose ();
				this.NameLabel = null;
			}
			if (this.ThumbImage != null) {
				this.ThumbImage.Dispose ();
				this.ThumbImage = null;
			}
		}
	}
}
