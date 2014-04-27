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
	}
}

