using System;
using MonoTouch.UIKit;
using Logic;
using MonoTouch.Foundation;
using System.Threading.Tasks;

namespace RottenTomatoes
{
	public class MovieDetailsViewController : UIViewController
	{
		private readonly IRottenTomatoesService _service;

		private MovieDetailsView _view;
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
			Title = Strings.DetailsScreenTitle;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_view.ScrollToTop();
			_view.HideContent();

			_service.GetMovieDetailsAsync(MovieId).ContinueWith(t =>
				InvokeOnMainThread(() => {
					_view.ShowContent();
					_view.BindMovieDetails(t.Result);
				}), TaskContinuationOptions.OnlyOnRanToCompletion);

			_service.GetMovieReviewsAsync(MovieId).ContinueWith(t =>
				InvokeOnMainThread(() => {
					_view.ShowContent();
					_view.BindCriticsReviews(t.Result);
				}), TaskContinuationOptions.OnlyOnRanToCompletion);

			_service.GetMovieFullCastAsync(MovieId).ContinueWith(t =>
				InvokeOnMainThread(() => {
					_view.ShowContent();
					_view.BindCast(t.Result);
				}), TaskContinuationOptions.OnlyOnRanToCompletion);
		}

		[BubbleEventHandler("moreClicked")]
		private void OnMoreClicked(object sender, ReviewEventArgs arg)
		{
			NSUrl url = new NSUrl(arg.Review.links.review);
			UIApplication.SharedApplication.OpenUrl(url);
		}
	}
}