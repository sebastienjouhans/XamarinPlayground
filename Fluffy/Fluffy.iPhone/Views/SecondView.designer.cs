// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fluffy.iPhone.Views
{
	[Register ("SecondView")]
	partial class SecondView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel InitVariables { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (InitVariables != null) {
				InitVariables.Dispose ();
				InitVariables = null;
			}
		}
	}
}
