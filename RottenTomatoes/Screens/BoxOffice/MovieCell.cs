using System;
using MonoTouch.UIKit;
using Logic;
using System.Drawing;

namespace RottenTomatoes
{
	public class MovieCell : UITableViewCell
	{
		private UILabel _movieTitle;
		private UIImageView _thumbnail, _freshImg, _rottenImg;
		private UILabel _criticScore, _actors, _mppaRuntime;
		private readonly RatingFormatter _ratingFormatter;
		private readonly ActorsFormatter _actorsFormatter;
		private readonly RuntimeFormatter _runtimeFormatter;

		public MovieCell(UITableViewCellStyle style, string reuseId)
			: base(style, reuseId)
		{
			_ratingFormatter = new RatingFormatter();
			_actorsFormatter = new ActorsFormatter();
			_runtimeFormatter = new RuntimeFormatter();

			_thumbnail = new UIImageView {
				BackgroundColor = UIColor.Gray
			};
			ContentView.AddSubview(_thumbnail);

			_movieTitle = new UILabel {
				TextColor = UIColor.Blue
			};
			ContentView.AddSubview(_movieTitle);

			_freshImg = ImageInitializer.InitImageView(ImgPath.Indicators.Fresh);
			_rottenImg = ImageInitializer.InitImageView(ImgPath.Indicators.Rotten);
			ContentView.AddSubviews(_freshImg, _rottenImg);
			_freshImg.Hidden = _rottenImg.Hidden = true;

			_criticScore = new UILabel();
			ContentView.AddSubview(_criticScore);

			_actors = new UILabel();
			ContentView.AddSubview(_actors);

			_mppaRuntime = new UILabel();
			ContentView.AddSubview(_mppaRuntime);
		}

		public void Bind(Movie movie)
		{
			_movieTitle.Text = movie.title;
			_movieTitle.SizeToFit();

			_criticScore.Text = _ratingFormatter.Format(movie.ratings.critics_score);
			_criticScore.SizeToFit();

			_actors.Text = _actorsFormatter.Format(2, movie.abridged_cast);
			_actors.SizeToFit();

			string runtime = _runtimeFormatter.Format(movie.runtime);
			_mppaRuntime.Text = string.Format("{0}, {1}", movie.mpaa_rating, runtime);
			_mppaRuntime.SizeToFit();

			LayoutSubviews();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			_thumbnail.Begin().Size(new SizeF(61f, 91f)).Commit();

			_movieTitle.Begin().AlignLeft(_thumbnail).FillRight().Commit();

			_mppaRuntime.Begin().AlignRight().AlignBottom().Commit();

		}
	}
}

