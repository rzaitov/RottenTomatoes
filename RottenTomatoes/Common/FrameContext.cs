using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

using Logic;

namespace RottenTomatoes
{
	public class FrameContext
	{
		private UIView View { get; set; }
		public RectangleF Frame;

		public RectangleF? ParentBounds;
		public RectangleF? RelativeFrame;

		public FrameContext(UIView view, UIView relativeView)
		{
			View = view;
			Frame = View.Frame;

			UIView parentView = Runtime.GetNSObject<UIView> (Messaging.IntPtr_objc_msgSend (view.Handle, Selector.GetHandle ("superview")));

			ParentBounds = parentView != null ? parentView.Bounds : (RectangleF?)null;
			RelativeFrame = relativeView != null ? relativeView.Frame : (RectangleF?)null;
		}

		public void Commit()
		{
			View.Frame = Frame;
		}
	}

	public static class FrameHelper
	{
		/// <summary>
		/// Get FrameContext
		/// </summary>
		public static FrameContext Begin(this UIView view, UIView relativeView = null)
		{
			FrameContext fc = new FrameContext(view, relativeView);
			return fc;
		}

		public static FrameContext Begin(this UIView view, UIView parentView, UIView relativeView = null)
		{
			FrameContext fc = new FrameContext(view, relativeView);
			return fc;
		}

		public static void Apply(this UIView view, Action<FrameContext> transformation)
		{
			var fc = view.Begin();
			transformation(fc);
			fc.Commit();
		}

		#region Coordinates and dimensions
		public static FrameContext X(this FrameContext fc, float x)
		{
			fc.Frame.X = x;
			return fc;
		}

		public static void X(this UIView view, float x)
		{
			view.Begin().X(x).Commit();
		}

		public static FrameContext Y(this FrameContext fc, float y)
		{
			fc.Frame.Y = y;
			return fc;
		}

		public static FrameContext Width(this FrameContext fc, float width)
		{
			fc.Frame.Width = width;
			return fc;
		}

		public static FrameContext Height(this FrameContext fc, float height)
		{
			fc.Frame.Height = height;
			return fc;
		}

		public static FrameContext Size(this FrameContext fc, SizeF size)
		{
			fc.Frame.Size = size;
			return fc;
		}

		public static FrameContext Size(this FrameContext fc, float width, float height)
		{
			return fc.Size(new SizeF(width, height));
		}

		#endregion

		#region Alignment
		public static FrameContext AlignLeft(this FrameContext fc, float dx = 0f)
		{
			fc.Frame.X = dx;
			return fc;
		}

		public static FrameContext AlignLeft(this FrameContext fc, UIView relativeView, float dx = 0f)
		{
			fc.Frame.X = relativeView.Frame.X + dx;
			return fc;
		}

		public static FrameContext AlignTop(this FrameContext fc, float topMargin = 0f)
		{
			fc.Frame.Y = topMargin;
			return fc;
		}

		public static FrameContext AlignTop(this FrameContext fc, UIView relativeView, float topMargin = 0f)
		{
			return fc.AlignTop(relativeView.Frame, topMargin);
		}

		public static FrameContext AlignTop(this FrameContext fc, RectangleF relativeViewFrame, float topMargin = 0f)
		{
			fc.Frame.Y = relativeViewFrame.Y + topMargin;
			return fc;
		}

		public static FrameContext AlignRight(this FrameContext fc, float rightMargin = 0f)
		{
			fc.Frame.X = fc.ParentBounds.Value.Width - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext AlignRight(this FrameContext fc, UIView relativeView, float rightMargin = 0f)
		{
			fc.Frame.X = relativeView.Frame.Right - fc.Frame.Width - rightMargin;
			return fc;
		}

		public static FrameContext AlignBottom(this FrameContext fc, float bottomMargin = 0f)
		{
			return fc.BMargin(bottomMargin);
		}

		public static FrameContext AlignBottom(this FrameContext fc, UIView relativeView, float bottomMargin = 0f)
		{
			fc.Frame.Y = relativeView.Frame.Bottom - fc.Frame.Height - bottomMargin;
			return fc;
		}
		#endregion

		#region Margin
		public static FrameContext RMargin(this FrameContext fc, float rightMargin)
		{
			fc.Frame.Width = fc.ParentBounds.Value.Right - fc.Frame.X - rightMargin;
			return fc;
		}

		public static FrameContext BMargin(this FrameContext fc, float bottomMargin)
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height - fc.Frame.Height - bottomMargin;
			return fc;
		}

		public static FrameContext LMargin(this FrameContext fc, float leftMargin)
		{
			fc.Frame.X = leftMargin;
			return fc;
		}

		public static FrameContext TMagrin(this FrameContext fc, float topMargin)
		{
			fc.Frame.Y = topMargin;
			return fc;
		}
		#endregion

		#region Placement
		public static FrameContext PlaceAbove(this FrameContext fc, UIView viewBelow, float dy = 0f)
		{
			fc.Frame.Y = viewBelow.Frame.Y - fc.Frame.Height + dy;
			return fc;
		}

		public static void PlaceAbove(this UIView view, UIView viewBelow, float dy = 0f)
		{
			view.Begin().PlaceAbove(viewBelow, dy).Commit();
		}

		public static FrameContext PlaceBelow(this FrameContext fc, float dy = 0f)
		{
			fc.Frame.Y = fc.ParentBounds.Value.Height + dy;
			return fc;
		}

		public static FrameContext PlaceBelow(this FrameContext fc, UIView viewAbove, float dy = 0f)
		{
			fc.Frame.Y = viewAbove.Frame.Bottom + dy;
			return fc;
		}

		public static FrameContext PlaceRight(this FrameContext fc, UIView pivot, float dx = 0f)
		{
			fc.Frame.X = pivot.Frame.Right + dx;
			return fc;
		}

		public static void PlaceRight(this UIView view, UIView pivot, float dx = 0f)
		{
			view.Begin().PlaceRight(pivot).Commit();
		}

		public static FrameContext PlaceLeft(this FrameContext fc, UIView pivot, float dx = 0f)
		{
			fc.Frame.X = pivot.Frame.X - fc.Frame.Width + dx;
			return fc;
		}

		public static void PlaceLeft(this UIView view, UIView pivot, float dx = 0f)
		{
			view.Begin().PlaceLeft(pivot, dx).Commit();
		}

		public static FrameContext CenterH(this FrameContext fc)
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

		public static FrameContext CenterV(this FrameContext fc)
		{
			fc.Frame.Y = (fc.ParentBounds.Value.Height - fc.Frame.Height) / 2;
			return fc;
		}

		public static void CenterV(this UIView view)
		{
			view.Begin().CenterV().Commit();
		}

		public static FrameContext Center(this FrameContext fc)
		{
			fc.CenterH();
			fc.CenterV();
			return fc;
		}

		public static FrameContext Location(this FrameContext fc, PointF location)
		{
			fc.Frame.Location = location;
			return fc;
		}

		/// <summary>
		/// Расплолагает view правее центра родителя. Есть возможность сдвига на dx
		/// </summary>
		public static FrameContext RightOfCenter(this FrameContext fc, float dx = 0f)
		{
			fc.Frame.X = fc.ParentBounds.Value.Width / 2 + dx;
			return fc;
		}
		#endregion

		#region Filling
		public static FrameContext FillHorizontally(this FrameContext fc, float left = 0f, float right = 0f)
		{
			fc.Frame.X = left;
			fc.Frame.Width = fc.ParentBounds.Value.Width - left - right;
			return fc;
		}

		public static void FillHorizontally(this UIView view, float left = 0f, float right = 0f)
		{
			view.Begin().FillHorizontally(left, right).Commit();
		}

		public static FrameContext FillVertically(this FrameContext fc, float top = 0f, float bottom = 0f)
		{
			fc.Frame.Y = top;
			fc.Height(fc.ParentBounds.Value.Height - top - bottom);
			return fc;
		}

		public static FrameContext FillBelow(this FrameContext fc, float bottomMargin = 0f)
		{
			float height = fc.ParentBounds.Value.Height - fc.Frame.Top - bottomMargin;
			Assert.True(height >= 0f);

			fc.Frame.Height = height;
			return fc;
		}

		public static FrameContext FillBelowUntil(this FrameContext fc, UIView stopView, float bottomMargin = 0f)
		{
			float height = stopView.Frame.Top - fc.Frame.Top - bottomMargin;
			Assert.True(height >= 0f);

			fc.Frame.Height = height;
			return fc;
		}

		/// <summary>
		/// Fill parent view
		/// </summary>
		public static FrameContext Fill(this FrameContext fc, float left = 0f, float top = 0f, float right = 0f, float bottom = 0f)
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
		public static FrameContext FillRight(this FrameContext fc)
		{
			fc.Frame.Width = fc.ParentBounds.Value.Width - fc.Frame.Left;
			return fc;
		}
		#endregion

		#region Movement
		public static FrameContext MoveX(this FrameContext fc, float dx)
		{
			fc.Frame.X += dx;
			return fc;
		}

		public static FrameContext MoveY(this FrameContext fc, float dy, float? minY = null, float? maxY = null)
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

