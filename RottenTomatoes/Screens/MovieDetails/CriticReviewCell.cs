using System;
using MonoTouch.UIKit;
using Logic;
using MonoTouch.ObjCRuntime;
using System.Drawing;

namespace RottenTomatoes
{
	public class CriticReviewCell : UITableViewCell
	{
		private UIImageView _freshIndicator, _rottenIndicator;
		private UILabel _criticPublication, _quote;
		private UIButton _more;

		private Review _review;

		public CriticReviewCell(UITableViewCellStyle style, string reuseId)
			: base(style, reuseId)
		{
			_freshIndicator = ImageInitializer.InitImageView(ImgPath.Indicators.FreshBig);
			_rottenIndicator = ImageInitializer.InitImageView(ImgPath.Indicators.RottenBig);
			ContentView.AddSubviews(_freshIndicator, _rottenIndicator);

			_criticPublication = UIFactory.MovieDetails.CriticPublicationLabel();
			_quote = UIFactory.MovieDetails.QuoteLabel();
			_more = UIFactory.MovieDetails.MoreLinkButton();
			_more.TouchUpInside += OnMoreClicked;

			ContentView.AddSubviews(_criticPublication, _quote);
			ContentView.AddSubview(_more);
		}

		private void OnMoreClicked (object sender, EventArgs e)
		{
			this.RaiseEvent("moreClicked", this, new ReviewEventArgs {
				Review = _review
			});
		}

		public void Bind(Review review)
		{
			_review = review;

			_freshIndicator.Hidden = !review.IsFresh;
			_rottenIndicator.Hidden = !review.IsRotten;

			_criticPublication.Text = string.Format("{0}, {1}", review.critic, review.publication);
			_quote.Text = review.quote;

			LayoutSubviews();
		}

		public override SizeF SizeThatFits(SizeF size)
		{
			SizeF s = new SizeF(size);
			s.Height = _more.Frame.Bottom;

			return s;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			_freshIndicator.Begin().CenterV().X(5f).Commit();
			_rottenIndicator.Begin().CenterV().X(5f).Commit();

			_criticPublication.Begin().PlaceRight(_freshIndicator, 5f).Commit().SizeToFit();
			_quote.Begin().PlaceRight(_freshIndicator, 5f).PlaceBelow(_criticPublication).FillRight().Commit().SizeToFit();
			_more.Begin().PlaceBelow(_quote).PlaceRight(_freshIndicator, 5f).Commit();
		}
	}
}

