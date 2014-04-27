using System;

using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public static class ImageInitializer
	{
		public static UIImageView InitImageView(string imagePath)
		{
			using(UIImage img = UIImage.FromFile(imagePath))
				return new UIImageView(img);
		}

		public static void InitImageView(string imagePath, UIImageView imgView)
		{
			Assert.NotNull(imgView);

			using (UIImage img = UIImage.FromFile(imagePath)) {
				imgView.Image = img;
				imgView.SizeToFit();
			}
		}

		public static UIImageView InitResizableImageView(string imagePath, UIEdgeInsets insetes)
		{
			using(UIImage img = InitResizableImage(imagePath, insetes))
				return new UIImageView(img);
		}

		public static UIImage InitResizableImage(string imagePath, UIEdgeInsets insetes)
		{
			using (UIImage img = UIImage.FromFile(imagePath))
				return img.CreateResizableImage(insetes);
		}

		public static bool TrySetImage(string imagePath,
		                               UIImageView imgView,
		                               bool errorIfNullOrWhitespace = false)
		{
			if (string.IsNullOrWhiteSpace(imagePath))
			{
				if (errorIfNullOrWhitespace)
					throw new ArgumentException();
				else
					return false;
			}

			using (UIImage img = UIImage.FromFile(imagePath))
			{
				imgView.Image = img;
				return img != null;
			}
		}

		public static bool TrySetImage(string imagePath,
		                               string defaultImagePath,
		                               UIImageView imgView,
		                               bool errorIfNullOrWhitespace = false)
		{
			if (string.IsNullOrWhiteSpace(imagePath) && errorIfNullOrWhitespace)
				throw new ArgumentException();

			bool isSuccess = TrySetImage(imagePath, imgView, errorIfNullOrWhitespace);
			if (!isSuccess)
				TrySetImage(defaultImagePath, imgView);

			return isSuccess;
		}

		public static void SetImageFor(string imagePath, UIButton button, UIControlState state)
		{
			using (UIImage img = UIImage.FromFile(imagePath))
			{
				Assert.NotNull(img);
				button.SetImage(img, state);
			}
		}

		public static void SetBgImageFor(string imagePath, UIButton button, UIControlState state)
		{
			using (UIImage img = UIImage.FromFile(imagePath))
			{
				Assert.NotNull(img);
				button.SetBackgroundImage(img, state);
			}
		}
	}
}