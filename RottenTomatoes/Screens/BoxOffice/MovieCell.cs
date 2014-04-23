using System;
using MonoTouch.UIKit;

using Logic;

namespace RottenTomatoes
{
	public class MovieCell : UITableViewCell
	{
		private UILabel _movieTitle;
		private UIImageView _thumbnail, _freshImg, _rottenImg;
		private UILabel _criticScore, _actors, _mppaRating, _runtime;

		private readonly RatingFormatter _ratingFormatter;

		public MovieCell (UITableViewCellStyle style, string reuseId)
			: base(style, reuseId)
		{
			_ratingFormatter = new RatingFormatter ();

			_movieTitle = new UILabel ();
			ContentView.AddSubview (_movieTitle);

//			_freshImg = ImageInitializer.InitImageView (ImgPath.Indicators.Fresh);
//			_rottenImg = ImageInitializer.InitImageView (ImgPath.Indicators.Rotten);
//			ContentView.AddSubviews (_freshImg, _rottenImg);

			_criticScore = new UILabel ();
			ContentView.AddSubview (_criticScore);

			_actors = new UILabel ();
			ContentView.AddSubview (_actors);

			_mppaRating = new UILabel ();
			ContentView.AddSubview (_mppaRating);

			_runtime = new UILabel ();
			ContentView.AddSubview (_runtime);
		}

		public void Bind(Movie movie)
		{
			_movieTitle.Text = movie.title;
			_movieTitle.SizeToFit ();

			_criticScore.Text = _ratingFormatter.Format(movie.ratings.critics_score);
			_criticScore.SizeToFit ();
		}
	}
}

