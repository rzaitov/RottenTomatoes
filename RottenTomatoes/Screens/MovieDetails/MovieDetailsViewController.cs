using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class MovieDetailsViewController : UIViewController
	{
		private readonly IRottenTomatoesService _service;

		private MovieDetailsView _view;

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
		}
	}
}