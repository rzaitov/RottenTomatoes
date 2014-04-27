using System;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace RottenTomatoes
{
	public class AttributedStringBuilder
	{
		private readonly NSMutableAttributedString _attrString;

		public AttributedStringBuilder()
		{
			_attrString = new NSMutableAttributedString();
		}

		public void Append(string text, UIFont font, UIColor color)
		{
			_attrString.Append(new NSAttributedString(text, font: font, foregroundColor: color));
		}

		public NSMutableAttributedString ToAttributedString()
		{
			return _attrString;
		}
	}
}