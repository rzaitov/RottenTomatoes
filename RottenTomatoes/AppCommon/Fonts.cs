using System;

using MonoTouch.UIKit;
using MonoTouch.CoreText;

namespace RottenTomatoes
{
	public static class Fonts
	{
		public static readonly UIFont Regular10 = HelveticaNeue(10f);
		public static readonly UIFont Regular11 = HelveticaNeue(11f);
		public static readonly UIFont Regular12 = HelveticaNeue(12f);
		public static readonly UIFont Regular13 = HelveticaNeue(13f);
		public static readonly UIFont Regular14 = HelveticaNeue(14f);

		public static readonly UIFont Bold10 = HelveticaNeueBold(10f);
		public static readonly UIFont Bold11 = HelveticaNeueBold(11f);
		public static readonly UIFont Bold12 = HelveticaNeueBold(12f);
		public static readonly UIFont Bold13 = HelveticaNeueBold(13f);
		public static readonly UIFont Bold14 = HelveticaNeueBold(14f);

		public static readonly UIFont Italic10 = HelveticaNeueItalic(10f);
		public static readonly UIFont Italic11 = HelveticaNeueItalic(11f);
		public static readonly UIFont Italic12 = HelveticaNeueItalic(12f);
		public static readonly UIFont Italic13 = HelveticaNeueItalic(13f);
		public static readonly UIFont Italic14 = HelveticaNeueItalic(14f);

		private static UIFont HelveticaNeue(float size)
		{
			return UIFont.FromName("HelveticaNeue", size);
		}

		private static UIFont HelveticaNeueBold(float size)
		{
			return UIFont.FromName("HelveticaNeue-Bold", size);
		}

		private static UIFont HelveticaNeueItalic(float size)
		{
			UIFont font = UIFont.FromName("HelveticaNeue-Italic", size);

			// http://stackoverflow.com/questions/19527962/what-happened-to-helveticaneue-italic-on-ios-7-0-3
			if (font == null) {
				var descriptor = UIFontDescriptor.FromName("HelveticaNeue-Italic", size);
				font = UIFont.FromDescriptor(descriptor, size);
			}

			return font;
		}
	}
}