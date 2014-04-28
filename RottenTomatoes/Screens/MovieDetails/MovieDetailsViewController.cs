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

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_service.GetMovieDetails("771362204", movieDetails =>
				InvokeOnMainThread(() => _view.BindMovieDetails(movieDetails)));

			_service.GetMovieReviews("771362204", reviews =>
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