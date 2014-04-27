﻿using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.Dialog.Utilities;

using Logic;
using System.Drawing;

namespace RottenTomatoes
{
	public class MovieDetailsView : UIView, IImageUpdated
	{
		private const float space = 5f;

		private UILabel _title;
		private UIImageView _profilePoster;

		private UIImageView _fresIndicator, _rottenIndicator, _usersIndicator;
		private UILabel _criticsScore, _usersScore;
		private UILabel _topReleaseDate;
		private UILabel _mppaRuntime;

		private UIScrollView _container;
		private UILabel _synopsis;
		private UILabel _cast;
		private UILabel _director;
		private UILabel _rated;
		private UILabel _runingTime;
		private UILabel _genre;
		private UILabel _release;

		private UITableView _table;
		private ReviewSource _source;

		private Uri _posterUri;

		public MovieDetailsView()
		{
			BackgroundColor = UIColor.White;

			_container = new UIScrollView {
				(_title = new UILabel()),
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
//			AddSubview(_container);

			_title.Font = Fonts.Bold14;
			_title.BackgroundColor = UIColor.Red;
			_title.TextColor = UIColor.White;

			ImageInitializer.InitImageView(ImgPath.Indicators.FreshSmall, _fresIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.RottenSmall, _rottenIndicator);
			ImageInitializer.InitImageView(ImgPath.Indicators.User, _usersIndicator);

			_topReleaseDate.Font = Fonts.Regular14;
			_mppaRuntime.Font = Fonts.Regular14;
			_criticsScore.Font = Fonts.Regular14;
			_usersScore.Font = Fonts.Regular14;

			_source = new ReviewSource();
			_table = new UITableView {
				Source = _source,
				SeparatorInset = UIEdgeInsets.Zero
			};
//			_table.RowHeight = 70f;
			AddSubview(_table);
		}

		public void BindMovieDetails(MovieDetails movie)
		{
			_posterUri = new Uri(movie.posters.profile);
			UIImage img = ImageLoader.DefaultRequestImage(_posterUri, this);
			_profilePoster.Image = img;

			_fresIndicator.Hidden = !movie.ratings.IsFresh;
			_rottenIndicator.Hidden = !movie.ratings.IsRotten;

			_title.Text = movie.title;
			_criticsScore.Text = GetScore(movie.ratings.critics_score, Strings.CriticsLiked);
			_usersScore.Text = GetScore(movie.ratings.audience_score, Strings.UsersLiked);

			var release = ReleaseDateFormatter.Format(movie.release_dates.theater);
			_topReleaseDate.Text = string.Format("{0} {1}", Strings.InTheaters, release);

			_mppaRuntime.Text = MpaaRuntimeFormatter.Format(movie.mpaa_rating, movie.runtime);

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

			SetTitleValueFor(_release, Strings.TheaterRelease, release);

			LayoutSubviews();
		}

		public void BindCriticsReviews(IList<Review> reviews)
		{
			_source.Reviews = reviews;
			_table.ReloadData();
		}

		public void UpdatedImage(Uri uri)
		{
			if (uri == _posterUri) {
				UIImage img = ImageLoader.DefaultRequestImage(uri, this);
				_profilePoster.Image = img;
			}
		}

		private string GetScore(int percent, string likedpostfix)
		{
			return string.Format("{0}% {1}", percent, likedpostfix);
		}

		private void SetTitleValueFor(UILabel label, string title, string value)
		{
			AttributedStringBuilder asb = new AttributedStringBuilder();
			asb.Append(title, Fonts.Bold14, UIColor.Black);
			asb.Append(": ", Fonts.Bold14, UIColor.Black);
			asb.Append(value, Fonts.Regular14, UIColor.Black);

			label.AttributedText = asb.ToAttributedString();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			/*
			_container.Begin().Fill().Commit();

			_title.SizeToFit();
			_title.Begin().FillHorizontally().Commit();
			_profilePoster.Begin().Size(120f, 178f).PlaceBelow(_title).Commit();

			_fresIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_title).Commit();
			_rottenIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_title).Commit();
			_criticsScore.Begin().PlaceRight(_fresIndicator, space).PlaceBelow(_title).Commit().SizeToFit();

			_usersIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_criticsScore).Commit();
			_usersScore.Begin().PlaceRight(_usersIndicator, space).PlaceBelow(_criticsScore).Commit().SizeToFit();

			_topReleaseDate.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_usersScore).Commit().SizeToFit();
			_mppaRuntime.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_topReleaseDate).Commit().SizeToFit();

			_synopsis.Begin().PlaceBelow(_profilePoster).FillHorizontally().Commit().SizeToFit();

			AppendToStack(_synopsis, _profilePoster);
			AppendToStack(_cast, _synopsis);
			AppendToStack(_director, _cast);
			AppendToStack(_rated, _director);
			AppendToStack(_runingTime, _rated);
			AppendToStack(_genre, _runingTime);
			AppendToStack(_release, _genre);

			_container.ContentSize = new SizeF(320f, _release.Frame.Bottom);
			*/
			_table.Begin().Fill().Commit();
		}

		private void AppendToStack(UILabel label, UIView topView)
		{
			label.Begin().PlaceBelow(topView).FillHorizontally().Commit();
			label.SizeToFit();
		}
	}
}