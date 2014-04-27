﻿using System;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class CriticReviewCell : UITableViewCell
	{
		private UIImageView _freshIndicator, _rottenIndicator;
		private UILabel _criticPublication, _quote;

		public CriticReviewCell(UITableViewCellStyle style, string reuseId)
			: base(style, reuseId)
		{
			_freshIndicator = ImageInitializer.InitImageView(ImgPath.Indicators.FreshBig);
			_rottenIndicator = ImageInitializer.InitImageView(ImgPath.Indicators.RottenBig);
			ContentView.AddSubviews(_freshIndicator, _rottenIndicator);

			_criticPublication = new UILabel();
			_criticPublication.Font = Fonts.Italic14;

			_quote = UIFactory.MultiLineLabel();
			_quote.Font = Fonts.Regular14;

			ContentView.AddSubviews(_criticPublication, _quote);
		}

		public void Bind(Review review)
		{
			_freshIndicator.Hidden = !review.IsFresh;
			_rottenIndicator.Hidden = !review.IsRotten;

			_criticPublication.Text = string.Format("{0}, {1}", review.critic, review.publication);
			_quote.Text = review.quote;

			LayoutSubviews();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			_freshIndicator.Begin().CenterV().X(5f).Commit();
			_rottenIndicator.Begin().CenterV().X(5f).Commit();

			_criticPublication.Begin().PlaceRight(_freshIndicator, 5f).Commit().SizeToFit();
			_quote.Begin().PlaceRight(_freshIndicator, 5f).PlaceBelow(_criticPublication).FillRight().Commit().SizeToFit();
		}
	}
}

