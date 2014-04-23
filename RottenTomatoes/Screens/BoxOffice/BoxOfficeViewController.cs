using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeViewController : UIViewController
	{
		private BoxOfficeView _view;

		private readonly RottenTomatoesService _service;

		public BoxOfficeViewController (RottenTomatoesService service)
		{
			_service = service;
		}

		public override void LoadView ()
		{
			base.LoadView ();

			_view = new BoxOfficeView ();
			View = _view;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_service.GetTopBoxOfficeAsync (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowMovies (movies);
				});
			});
		}
	}
}

