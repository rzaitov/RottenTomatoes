using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Dialog.Utilities;

using Logic;

namespace RottenTomatoes
{
	public class MovieCell : UITableViewCell, IImageUpdated
	{
		private const float LeftMargin = 5f;
		private const float VerticalSpace = 2f;
		private readonly SizeF ThumbSize = new SizeF(61f, 91f);

		private UILabel _movieTitle;
		private UIImageView _thumbnail, _freshImg, _rottenImg;
		private UILabel _criticScore, _actors, _mppaRuntime;

		private readonly RatingFormatter _ratingFormatter;
		private readonly ActorsFormatter _actorsFormatter;

		private Uri _imgUri;

		public MovieCell(UITableViewCellStyle style, string reuseId)
			: base(style, reuseId)
		{
			_ratingFormatter = new RatingFormatter();
			_actorsFormatter = new ActorsFormatter();

			_thumbnail = new UIImageView {
				BackgroundColor = UIColor.Gray
			};
			ContentView.AddSubview(_thumbnail);

			_movieTitle = new UILabel {
				TextColor = UIColor.Blue,
				Font = Fonts.Bold14
			};
			ContentView.AddSubview(_movieTitle);

			_freshImg = ImageInitializer.InitImageView(ImgPath.Indicators.Fresh);
			_rottenImg = ImageInitializer.InitImageView(ImgPath.Indicators.Rotten);
			ContentView.AddSubviews(_freshImg, _rottenImg);
//			_freshImg.Hidden = _rottenImg.Hidden = true;

			_criticScore = InitInfoLabel();
			_actors = InitInfoLabel();
			_mppaRuntime = InitInfoLabel();
		}

		private UILabel InitInfoLabel()
		{
			var lbl = new UILabel {
				BackgroundColor = UIColor.Clear,
				Font = Fonts.Regular14
			};
			ContentView.AddSubview(lbl);

			return lbl;
		}

		public void Bind(Movie movie)
		{
			bool isFresh = movie.ratings.IsFresh;
			bool indicatorExists = !string.IsNullOrWhiteSpace(movie.ratings.critics_rating);
			_freshImg.Hidden = !isFresh || !indicatorExists;
			_rottenImg.Hidden = isFresh || !indicatorExists;

			_movieTitle.Text = movie.title;
			_movieTitle.SizeToFit();

			_criticScore.Text = _ratingFormatter.Format(movie.ratings.critics_score);
			_criticScore.SizeToFit();

			_actors.Text = _actorsFormatter.Format(2, movie.abridged_cast);
			_actors.SizeToFit();

			_mppaRuntime.Text = MpaaRuntimeFormatter.Format(movie.mpaa_rating, movie.runtime);
			_mppaRuntime.SizeToFit();

			_imgUri = new Uri(movie.posters.thumbnail);

			UIImage img = ImageLoader.DefaultRequestImage(_imgUri, this);
			_thumbnail.Image = img;

			LayoutSubviews();
		}

		public void UpdatedImage(Uri uri)
		{
			if (_imgUri == uri) {
				UIImage img = ImageLoader.DefaultRequestImage(uri, this);
				_thumbnail.Image = img;
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			_thumbnail.Begin().Size(ThumbSize).Commit();

			_movieTitle.Begin().Y(VerticalSpace).PlaceRight(_thumbnail, LeftMargin).FillRight().Commit();
			_actors.Begin().PlaceRight(_thumbnail, LeftMargin).PlaceBelow(_movieTitle, VerticalSpace).Commit();

			_freshImg.Begin().PlaceRight(_thumbnail, LeftMargin).PlaceBelow(_actors, VerticalSpace).Commit();
			_rottenImg.Begin().PlaceRight(_thumbnail, LeftMargin).PlaceBelow(_actors, VerticalSpace).Commit();

			_criticScore.Begin().PlaceRight(_freshImg, LeftMargin).PlaceBelow(_actors, VerticalSpace).Commit();
			_mppaRuntime.Begin().PlaceRight(_thumbnail, LeftMargin).PlaceBelow(_criticScore, VerticalSpace).Commit();
		}
	}
}

