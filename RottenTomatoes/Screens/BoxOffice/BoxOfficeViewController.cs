using System;
using MonoTouch.UIKit;
using Logic;
using System.Threading.Tasks;

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
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, OnRefresh);

			RequestInfoAsync();
		}

		[BubbleEventHandler("moveClicked")]
		private void OnMovieClicked(object sender, MovieEventArgs e)
		{
			_detailsViewController = _detailsViewController ?? new MovieDetailsViewController(_service);
			_detailsViewController.MovieId = e.Movie.id;
			NavigationController.PushViewController(_detailsViewController, true);
		}

		private void OnRefresh(object sender, EventArgs arg)
		{
			RequestInfoAsync();
		}

		private void RequestInfoAsync()
		{
			_service.GetOpeningThisWeekAsync().ContinueWith(t => {
				BeginInvokeOnMainThread (() => {
				_view.ShowOpeningThisWeek(t.Result);
				});
			}, TaskContinuationOptions.OnlyOnRanToCompletion);

			_service.GetTopBoxOfficeAsync().ContinueWith(t => {
				BeginInvokeOnMainThread (() => {
					_view.ShowTopBoxOffice (t.Result);
				});
			}, TaskContinuationOptions.OnlyOnRanToCompletion);

			_service.GetInTheatersAsync().ContinueWith(t => {
				BeginInvokeOnMainThread (() => {
					_view.ShowInTheaters (t.Result);
				});
			}, TaskContinuationOptions.OnlyOnRanToCompletion);
		}
	}
}

