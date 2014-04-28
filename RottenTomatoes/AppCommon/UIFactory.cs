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

			public static UILabel LeadSectionHeader()
			{
				UILabel lbl = new UILabel();
				lbl.Font = Fonts.Bold14;
				lbl.BackgroundColor = UIColor.Red;
				lbl.TextColor = UIColor.White;

				return lbl;
			}

			public static UILabel SlaveSectionHeader(string header)
			{
				UILabel lbl = new UILabel();
				lbl.Font = Fonts.Bold14;
				lbl.BackgroundColor = UIColor.Black;
				lbl.TextColor = UIColor.White;
				lbl.Text = header;

				return lbl;
			}
		}
	}
}

