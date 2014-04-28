using System;

using MonoTouch.UIKit;

namespace RottenTomatoes
{
	public static class UIFactory
	{
		public static UILabel MultiLineLabel()
		{
			var lbl = new UILabel();
			lbl.BackgroundColor = UIColor.Clear;

			lbl.Lines = 0;
			lbl.LineBreakMode = UILineBreakMode.WordWrap;

			return lbl;
		}

		public static class MovieDetails
		{
			public static UIButton MoreLinkButton()
			{
				var btn = new UIButton();

				btn.SetTitle(Strings.More, UIControlState.Normal);
				btn.SetTitleColor(UIColor.Blue, UIControlState.Normal);

				btn.Font = Fonts.Regular14;
//				btn.BackgroundColor = UIColor.DarkGray;

				btn.SizeToFit();

				return btn;
			}

			public static UILabel CriticPublicationLabel()
			{
				var lbl = new UILabel();
				lbl.Font = Fonts.Italic14;
//				lbl.BackgroundColor = UIColor.Cyan;
				return lbl;
			}

			public static UILabel QuoteLabel()
			{
				var quoteLbl = UIFactory.MultiLineLabel();
				quoteLbl.Font = Fonts.Regular14;
//				quoteLbl.BackgroundColor = UIColor.Green;

				return quoteLbl;
			}
		}
	}
}

