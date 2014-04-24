using System;

using MonoTouch.UIKit;

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

		private static UIFont HelveticaNeue(float size)
		{
			return UIFont.FromName("HelveticaNeue", size);
		}

		private static UIFont HelveticaNeueBold(float size)
		{
			return UIFont.FromName("HelveticaNeue-Bold", size);
		}

	}
}