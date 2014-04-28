using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeViewController : UIViewController
	{
		private BoxOfficeView _view;

		private readonly IRottenTomatoesService _service;
		private MovieDetailsViewController _detailsViewController;

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

			Title = Strings.BoxOfficeScreenTitle;

			_service.GetOpeningThisWeekAsync (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowOpeningThisWeek (movies);
				});
			});

			_service.GetTopBoxOfficeAsync (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowTopBoxOffice (movies);
				});
			});

			_service.GetInTheatersAsync (movies => {
				BeginInvokeOnMainThread (() => {
					_view.ShowInTheaters (movies);
				});
			});
		}

		[BubbleEventHandler("moveClicked")]
		private void OnMovieClicked(object sender, MovieEventArgs e)
		{
			_detailsViewController = _detailsViewController ?? new MovieDetailsViewController(_service);
			_detailsViewController.MovieId = e.Movie.id;
			NavigationController.PushViewController(_detailsViewController, true);
		}
	}
}

