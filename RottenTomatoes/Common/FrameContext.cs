using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

using Logic;

namespace RottenTomatoes
{
	public class FrameContext<T> where T : UIView
	{
		private T View { get; set; }
		public RectangleF Frame;

		public RectangleF? ParentBounds;
		public RectangleF? RelativeFrame;

		public FrameContext(T view, UIView relativeView)
		{
			View = view;
			Frame = View.Frame;

			UIView parentView = Runtime.GetNSObject<UIView> (Messaging.IntPtr_objc_msgSend (view.Handle, Selector.GetHandle ("superview")));

			ParentBounds = parentView != null ? parentView.Bounds : (RectangleF?)null;
			RelativeFrame = relativeView != null ? relativeView.Frame : (RectangleF?)null;
		}

		public T Commit()
		{
			View.Frame = Frame;
			return View;
		}
	}

	public static class FrameHelper
	{
		/// <summary>
		/// Get FrameContext
		/// </summary>
		public static FrameContext<T> Begin<T>(this T view, UIView relativeView = null) where T : UIView
		{
			FrameContext<T> fc = new FrameContext<T>(view, relativeView);
			return fc;
		}

		public static FrameContext<T> Begin<T>(this T view, UIView parentView, UIView relativeView = null)  where T : UIView
		{
			FrameContext<T> fc = new FrameContext<T>(view, relativeView);
			return fc;
		}

		#region Coordinates and dimensions
		public static FrameContext<T> X<T>(this FrameContext<T> fc, float x) where T : UIView
		{
			fc.Frame.X = x;
			return fc;
		}

		public static void X(this UIView view, float x)
		{
			view.Begin().X(x).Commit();
		}

		public static FrameContext<T> Y<T>(this FrameContext<T> fc, float y) where T : UIView
		{
			fc.Frame.Y = y;
			return fc;
		}

		public static FrameContext<T> Width<T>(this FrameContext<T> fc, float width) where T : UIView
		{
			fc.Frame.Width = width;
			return fc;
		}

		public static FrameContext<T> Height<T>(this FrameContext<T> fc, float height) where T : UIView
		{
			fc.Frame.Height = height;
			return fc;
		}

		public static FrameContext<T> Size<T>(this FrameContext<T> fc, SizeF size) where T : UIView
		{
			fc.Frame.Size = size;
			return fc;
		}

		public static FrameContext<T> Size<T>(this FrameContext<T> fc, float width, float height) where T : UIView
		{
			return fc.Size(new SizeF(width, height));
		}

		#endregion

		#region Alignment
		public static FrameContext<T> AlignLeft<T>(this FrameContext<T> fc, float dx = 0f) where T : UIView
		{
			fc.Frame.X = dx;
			return fc;
		}

		public static FrameContext<T> AlignLeft<T>(this FrameContext<T> fc, UIView relativeView, float dx = 0f) where T : UIView
		{
			fc.Frame.X = relativeView.Frame.X + dx;
			return fc;
		}

		public static FrameContext<T> AlignTop<T>(this FrameContext<T> fc, float topMargin = 0f) where T : UIView
		{
			fc.Frame.Y = topMargin;
			return fc;
		}

		public static FrameContext<T> AlignTop<T>(this FrameContext<T> fc, UIView relativeView, float topMargin = 0f) where T : UIView
		{
			return fc.AlignTop(relativeView.Frame, topMargin);
		}

		public static FrameContext<T> AlignTop<T>(this FrameContext<T> fc, RectangleF relativeViewFrame, float topMargin = 0f) where T : UIView
		{
			fc.Frame.Y = relativeViewFrame.Y + topMargin;
			return fc;
		}

		public static FrameContext<T> AlignRight<T>(this FrameContext<T> fc, float rightMargin = 0f) where T : UIView
		{
			fc.Frame.X = fc.ParentBounds.Value.Width - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext<T> AlignRight<T>(this FrameContext<T> fc, UIView relativeView, float rightMargin = 0f) where T : UIView
		{
			fc.Frame.X = relativeView.Frame.Right - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext<T> AlignBottom<T>(this FrameContext<T> fc, float bottomMargin = 0f) where T : UIView
		{
			return fc.BMargin(bottomMargin);
		}

		public static FrameContext<T> AlignBottom<T>(this FrameContext<T> fc, UIView relativeView, float bottomMargin = 0f) where T : UIView
		{
			fc.Frame.Y = relativeView.Frame.Bottom - fc.Frame.Height - bottomMargin;
			return fc;
		}
		#endregion

		#region Margin
		public static FrameContext<T> RMargin<T>(this FrameContext<T> fc, float rightMargin) where T : UIView
		{
			fc.Frame.Width = fc.ParentBounds.Value.Right - fc.Frame.X - rightMargin;
			return fc;
		}

		public static FrameContext<T> BMargin<T>(this FrameContext<T> fc, float bottomMargin) where T : UIView
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height - fc.Frame.Height - bottomMargin;
			return fc;
		}

		public static FrameContext<T> LMargin<T>(this FrameContext<T> fc, float leftMargin) where T : UIView
		{
			fc.Frame.X = leftMargin;
			return fc;
		}

		public static FrameContext<T> TMagrin<T>(this FrameContext<T> fc, float topMargin) where T : UIView
		{
			fc.Frame.Y = topMargin;
			return fc;
		}
		#endregion

		#region Placement
		public static FrameContext<T> PlaceAbove<T>(this FrameContext<T> fc, UIView viewBelow, float dy = 0f) where T : UIView
		{
			fc.Frame.Y = viewBelow.Frame.Y - fc.Frame.Height + dy;
			return fc;
		}

		public static void PlaceAbove<T>(this UIView view, UIView viewBelow, float dy = 0f)
		{
			view.Begin().PlaceAbove(viewBelow, dy).Commit();
		}

		public static FrameContext<T> PlaceBelow<T>(this FrameContext<T> fc, float dy = 0f) where T : UIView
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height + dy;
			return fc;
		}

		public static FrameContext<T> PlaceBelow<T>(this FrameContext<T> fc, UIView viewAbove, float dy = 0f) where T : UIView
		{
			fc.Frame.Y = viewAbove.Frame.Bottom + dy;
			return fc;
		}

		public static FrameContext<T> PlaceRight<T>(this FrameContext<T> fc, UIView pivot, float dx = 0f) where T : UIView
		{
			fc.Frame.X = pivot.Frame.Right + dx;
			return fc;
		}

		public static void PlaceRight(this UIView view, UIView pivot, float dx = 0f)
		{
			view.Begin().PlaceRight(pivot).Commit();
		}

		public static FrameContext<T> PlaceLeft<T>(this FrameContext<T> fc, UIView pivot, float dx = 0f) where T : UIView
		{
			fc.Frame.X = pivot.Frame.X - fc.Frame.Width + dx;
			return fc;
		}

		public static void PlaceLeft(this UIView view, UIView pivot, float dx = 0f)
		{
			view.Begin().PlaceLeft(pivot, dx).Commit();
		}

		public static FrameContext<T> CenterH<T>(this FrameContext<T> fc) where T : UIView
		{
			fc.Frame.X = (fc.ParentBounds.Value.Width - fc.Frame.Width) / 2;
			return fc;
		}

		public static void CenterH(this UIView view)
		{
			view.Begin().CenterH().Commit();
		}

		public static void CenterH(params UIView[] views)
		{
			foreach (var v in views)
				v.CenterH();
		}

		public static FrameContext<T> CenterV<T>(this FrameContext<T> fc) where T : UIView
		{
			fc.Frame.Y = (fc.ParentBounds.Value.Height - fc.Frame.Height) / 2;
			return fc;
		}

		public static void CenterV(this UIView view)
		{
			view.Begin().CenterV().Commit();
		}

		public static FrameContext<T> Center<T>(this FrameContext<T> fc) where T : UIView
		{
			fc.CenterH();
			fc.CenterV();
			return fc;
		}

		public static FrameContext<T> Location<T>(this FrameContext<T> fc, PointF location) where T : UIView
		{
			fc.Frame.Location = location;
			return fc;
		}

		/// <summary>
		/// Расплолагает view правее центра родителя. Есть возможность сдвига на dx
		/// </summary>
		public static FrameContext<T> RightOfCenter<T>(this FrameContext<T> fc, float dx = 0f) where T : UIView
		{
			fc.Frame.X = fc.ParentBounds.Value.Width / 2 + dx;
			return fc;
		}
		#endregion

		#region Filling
		public static FrameContext<T> FillHorizontally<T>(this FrameContext<T> fc, float left = 0f, float right = 0f) where T : UIView
		{
			fc.Frame.X = left;
			fc.Frame.Width = fc.ParentBounds.Value.Width - left - right;
			return fc;
		}

		public static void FillHorizontally(this UIView view, float left = 0f, float right = 0f)
		{
			view.Begin().FillHorizontally(left, right).Commit();
		}

		public static FrameContext<T> FillVertically<T>(this FrameContext<T> fc, float top = 0f, float bottom = 0f) where T : UIView
		{
			fc.Frame.Y = top;
			fc.Height(fc.ParentBounds.Value.Height - top - bottom);
			return fc;
		}

		public static FrameContext<T> FillBelow<T>(this FrameContext<T> fc, float bottomMargin = 0f) where T : UIView
		{
			float height = fc.ParentBounds.Value.Height - fc.Frame.Top - bottomMargin;
			Assert.True(height >= 0f);

			fc.Frame.Height = height;
			return fc;
		}

		public static FrameContext<T> FillBelowUntil<T>(this FrameContext<T> fc, UIView stopView, float bottomMargin = 0f) where T : UIView
		{
			float height = stopView.Frame.Top - fc.Frame.Top - bottomMargin;
			Assert.True(height >= 0f);

			fc.Frame.Height = height;
			return fc;
		}

		/// <summary>
		/// Fill parent view
		/// </summary>
		public static FrameContext<T> Fill<T>(this FrameContext<T> fc, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f) where T : UIView
		{
			fc.Frame = fc.ParentBounds.Value;
			fc.Frame.X += left;
			fc.Frame.Y += top;
			fc.Frame.Width -= left + right;
			fc.Frame.Height -= top + bottom;

			return fc;
		}

		/// <summary>
		/// Fill space to the right of the left border
		/// </summary>
		public static FrameContext<T> FillRight<T>(this FrameContext<T> fc) where T : UIView
		{
			fc.Frame.Width = fc.ParentBounds.Value.Width - fc.Frame.Left;
			return fc;
		}
		#endregion

		#region Movement
		public static FrameContext<T> MoveX<T>(this FrameContext<T> fc, float dx) where T : UIView
		{
			fc.Frame.X += dx;
			return fc;
		}

		public static FrameContext<T> MoveY<T>(this FrameContext<T> fc, float dy, float? minY = null, float? maxY = null) where T : UIView
		{
			fc.Frame.Y += dy;

			if (minY.HasValue && fc.Frame.Y < minY.Value)
				fc.Frame.Y = minY.Value;

			if(maxY.HasValue && fc.Frame.Y > maxY.Value)
				fc.Frame.Y = maxY.Value;

			return fc;
		}
		#endregion
	}
}

