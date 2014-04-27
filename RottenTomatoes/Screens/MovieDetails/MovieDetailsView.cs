using System;
using System.Collections.Generic;

using MonoTouch.UIKit;

using Logic;

namespace RottenTomatoes
{
	public class MovieDetailsView : UIView
	{
		private UILabel _title;
		private UIImageView _profilePoster;

		private UIImageView _fresIndicator, _rottenIndicator, _usersIndicator;
		private UILabel _criticsScore, _usersScore;
		private UILabel _topReleaseDate;
		private UILabel _mppaRuntime;

		private UIView _container;
		private UILabel _synopsis;
		private UILabel _cast;
		private UILabel _director;
		private UILabel _rated;
		private UILabel _genre;
		private UILabel _release;

		public MovieDetailsView()
		{
			_container = new UIView {
				(_title = new UILabel())
				(_profilePoster = new UIImageView()),

				(_fresIndicator = new UIImageView()),
				(_rottenIndicator = new UIImageView()),
				(_criticsScore = new UILabel()),

				(_usersIndicator = new UIImageView()),
				(_usersScore = new UILabel()),

				(_topReleaseDate = new UILabel()),
				(_mppaRuntime = new UILabel()),

				(_synopsis = new UILabel()),
				(_cast = new UILabel()),
				(_director = new UILabel()),
				(_rated = new UILabel()),
				(_genre = new UILabel()),
				(_release = new UILabel())
			};

			_title.Font = Fonts.Bold14;
			_title.BackgroundColor = UIColor.Red;

			ImageInitializer.InitImageView(ImgPath.Indicators.Fresh, _fresIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.Rotten, _rottenIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.User, _usersIndicator);

			_topReleaseDate.Font = Fonts.Regular14;
			_mppaRuntime.Font = Fonts.Regular14;
		}

		public void BindMovieDetails(Movie movie)
		{

		}

		public void BindCriticsReviews(IList<Review> reviews)
		{

		}
	}
}

