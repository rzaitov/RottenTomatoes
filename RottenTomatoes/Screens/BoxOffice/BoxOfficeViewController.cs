using System;
using MonoTouch.UIKit;

namespace RottenTomatoes
{
	public class BoxOfficeViewController : UIViewController
	{
		private BoxOfficeView _view;

		public BoxOfficeViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_view = new BoxOfficeView ();
			View = _view;
		}
	}
}

