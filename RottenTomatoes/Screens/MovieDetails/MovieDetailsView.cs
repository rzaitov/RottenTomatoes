using System;
using System.Collections.Generic;

using MonoTouch.UIKit;

using Logic;
using MonoTouch.Foundation;

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
		private UILabel _runingTime;
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

				(_synopsis = UIFactory.MultiLineLabel()),
				(_cast = UIFactory.MultiLineLabel()),
				(_director = UIFactory.MultiLineLabel()),
				(_rated = UIFactory.MultiLineLabel()),
				(_runingTime = UIFactory.MultiLineLabel()),
				(_genre = UIFactory.MultiLineLabel()),
				(_release = UIFactory.MultiLineLabel())
			};

			_title.Font = Fonts.Bold14;
			_title.BackgroundColor = UIColor.Red;

			ImageInitializer.InitImageView(ImgPath.Indicators.Fresh, _fresIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.Rotten, _rottenIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.User, _usersIndicator);

			_topReleaseDate.Font = Fonts.Regular14;
			_mppaRuntime.Font = Fonts.Regular14;
		}

		public void BindMovieDetails(MovieDetails movie)
		{
			SetTitleValueFor(_synopsis, Strings.Synopsis, movie.synopsis);

			ActorsFormatter af = new ActorsFormatter();

			string cast = af.Format(movie.abridged_cast);
			SetTitleValueFor(_cast, Strings.Cast, cast);

			string directors = af.Format(movie.abridged_directors);
			SetTitleValueFor(_director, Strings.Director, directors);

			SetTitleValueFor(_rated, Strings.Rated, movie.mpaa_rating);

			var rf = new RuntimeFormatter();
			string runtime = rf.Format(movie.runtime);
			SetTitleValueFor(_runingTime, Strings.Runtime, runtime);

			string genre = string.Join(", ", movie.genres);
			SetTitleValueFor(_genre, Strings.Genre, genre);

			string release = ReleaseDateFormatter.Format(movie.release_dates.theater);
			SetTitleValueFor(_runingTime, Strings.Runtime, release);
		}

		public void BindCriticsReviews(IList<Review> reviews)
		{

		}

		private void SetTitleValueFor(UILabel label, string title, string value)
		{
			AttributedStringBuilder asb = new AttributedStringBuilder();
			asb.Append(title, Fonts.Bold14, UIColor.Black);
			asb.Append(" ", Fonts.Regular14, UIColor.Black);
			asb.Append(value, Fonts.Regular14);

			label.AttributedText = asb.ToAttributedString();
			label.SizeToFit();
		}
	}
}