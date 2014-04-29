using System;
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

		private UIView _container;
		private UILabel _movieInfoHeader;
		private UILabel _synopsis;
		private UILabel _cast;
		private UILabel _director;
		private UILabel _rated;
		private UILabel _runingTime;
		private UILabel _genre;
		private UILabel _release;
		private UILabel _criticReviewsHeader;

		private UITableView _table;
		private ReviewSource _source;

		private Uri _posterUri;

		public MovieDetailsView()
		{
			BackgroundColor = UIColor.White;

			_container = new UIView {
				(_title = UIFactory.MovieDetails.LeadSectionHeader()),
				(_profilePoster = new UIImageView()),

				(_fresIndicator = new UIImageView()),
				(_rottenIndicator = new UIImageView()),
				(_criticsScore = new UILabel()),

				(_usersIndicator = new UIImageView()),
				(_usersScore = new UILabel()),

				(_topReleaseDate = new UILabel()),
				(_mppaRuntime = new UILabel()),


				(_movieInfoHeader = UIFactory.MovieDetails.SlaveSectionHeader(Strings.MovieInfoHeader)),
				(_synopsis = UIFactory.MultiLineLabel()),
				(_cast = UIFactory.MultiLineLabel()),
				(_director = UIFactory.MultiLineLabel()),
				(_rated = UIFactory.MultiLineLabel()),
				(_runingTime = UIFactory.MultiLineLabel()),
				(_genre = UIFactory.MultiLineLabel()),
				(_release = UIFactory.MultiLineLabel()),

				(_criticReviewsHeader = UIFactory.MovieDetails.SlaveSectionHeader(Strings.CriticReviewsHeader)),
			};

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
				SeparatorInset = UIEdgeInsets.Zero,
				TableHeaderView = _container
			};
			AddSubview(_table);
		}

		public void HideContent()
		{
			_table.Hidden = true;
		}

		public void ShowContent()
		{
			_table.Hidden = false;
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
			_criticsScore.Hidden = _fresIndicator.Hidden && _rottenIndicator.Hidden;

			_usersScore.Text = GetScore(movie.ratings.audience_score, Strings.UsersLiked);

			var release = ReleaseDateFormatter.Format(movie.release_dates.theater);
			_topReleaseDate.Text = string.Format("{0} {1}", Strings.InTheaters, release);

			_mppaRuntime.Text = MpaaRuntimeFormatter.Format(movie.mpaa_rating, movie.runtime);

			SetTitleValueFor(_synopsis, Strings.Synopsis, movie.synopsis);

			string directors = PersonFormatter.Format(movie.abridged_directors);
			SetTitleValueFor(_director, Strings.Director, directors);

			SetTitleValueFor(_rated, Strings.Rated, movie.mpaa_rating);

			string runtime = RuntimeFormatter.Format(movie.runtime);
			SetTitleValueFor(_runingTime, Strings.Runtime, runtime);

			string genre = string.Join(", ", movie.genres);
			SetTitleValueFor(_genre, Strings.Genre, genre);

			SetTitleValueFor(_release, Strings.TheaterRelease, release);

			BindCast(movie.abridged_cast);
		}

		public void BindCast(IList<Cast> cast)
		{
			string joinedCast = PersonFormatter.Format(cast);
			SetTitleValueFor(_cast, Strings.Cast, joinedCast);

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

		public void ScrollToTop()
		{
			_table.ContentOffset = PointF.Empty;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			_container.Begin().FillHorizontally().Commit();

			LayoutHeader(_title);

			_profilePoster.Begin().Size(120f, 178f).PlaceBelow(_title).Commit();

			_fresIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_title).Commit();
			_rottenIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_title).Commit();
			_criticsScore.Begin().PlaceRight(_fresIndicator, space).PlaceBelow(_title).Commit().SizeToFit();

			_usersIndicator.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_criticsScore).Commit();
			_usersScore.Begin().PlaceRight(_usersIndicator, space).PlaceBelow(_criticsScore).Commit().SizeToFit();

			_topReleaseDate.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_usersScore).Commit().SizeToFit();
			_mppaRuntime.Begin().PlaceRight(_profilePoster, space).PlaceBelow(_topReleaseDate).Commit().SizeToFit();

			_synopsis.Begin().PlaceBelow(_profilePoster).FillHorizontally().Commit().SizeToFit();

			AppendToStack(_movieInfoHeader, _profilePoster);
			AppendToStack(_synopsis, _movieInfoHeader);
			AppendToStack(_cast, _synopsis);
			AppendToStack(_director, _cast);
			AppendToStack(_rated, _director);
			AppendToStack(_runingTime, _rated);
			AppendToStack(_genre, _runingTime);
			AppendToStack(_release, _genre);
			AppendToStack(_criticReviewsHeader, _release);

			LayoutHeader(_movieInfoHeader);
			LayoutHeader(_criticReviewsHeader);

			_container.Begin().Height(_criticReviewsHeader.Frame.Bottom).Commit();
			_table.TableHeaderView = _container;
			_table.Begin().Fill().Commit();
		}

		private void LayoutHeader(UILabel header)
		{
			header.SizeToFit();
			header.Begin().FillHorizontally().Commit();
		}

		private void AppendToStack(UILabel label, UIView topView)
		{
			label.Begin().PlaceBelow(topView).FillHorizontally().Commit();
			label.SizeToFit();
		}
	}
}