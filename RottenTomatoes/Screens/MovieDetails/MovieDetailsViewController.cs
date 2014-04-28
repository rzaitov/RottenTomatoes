using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class MovieDetailsViewController : UIViewController
	{
		private readonly IRottenTomatoesService _service;

		private MovieDetailsView _view;
		private WebViewController _reviewController;

		public string MovieId { get; set; }

		public MovieDetailsViewController(IRottenTomatoesService service)
		{
			_service = service;
		}

		public override void LoadView()
		{
			base.LoadView();

			_view = new MovieDetailsView();
			View = _view;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Details";
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_view.ScrollToTop();

			_service.GetMovieDetails(MovieId, movieDetails =>
				InvokeOnMainThread(() => _view.BindMovieDetails(movieDetails)));

			_service.GetMovieReviews(MovieId, reviews =>
				InvokeOnMainThread(() => _view.BindCriticsReviews(reviews)));
		}

		[BubbleEventHandler("moreClicked")]
		private void OnMoreClicked(object sender, ReviewEventArgs arg)
		{
			_reviewController = _reviewController ?? new WebViewController();
			_reviewController.ReviewUrl = arg.Review.links.review;

			NavigationController.PushViewController(_reviewController, true);
		}
	}
}