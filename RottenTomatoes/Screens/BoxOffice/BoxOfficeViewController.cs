using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeViewController : UIViewController
	{
		private BoxOfficeView _view;

		private readonly IRottenTomatoesService _service;

		public BoxOfficeViewController (IRottenTomatoesService service)
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

			_service.GetOpeningThisWeek (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowOpeningThisWeek (movies);
				});
			});

			_service.GetTopBoxOfficeAsync (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowTopBoxOffice (movies);
				});
			});

			_service.GetInTheaters (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowInTheaters (movies);
				});
			});

		}
	}
}

