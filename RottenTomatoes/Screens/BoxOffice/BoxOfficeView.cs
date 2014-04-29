using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Logic;
using MonoTouch.Foundation;

namespace RottenTomatoes
{
	public class BoxOfficeView : UIView
	{
		private UITableView _table;
		private BoxOfficeSource _source;

		public BoxOfficeView ()
		{
			this.AccessibilityLabel = "BoxOfficeScreen";

			BackgroundColor = UIColor.White;

			_table = new UITableView ();
			_table.Source = _source = new BoxOfficeSource ();
			_table.RowHeight = 91f;
			_table.SeparatorInset = UIEdgeInsets.Zero;

			AddSubview (_table);
		}

		public void ShowOpeningThisWeek(IList<Movie> movies)
		{
			_source.OpeningThisWeek = movies;
			ReloadSection(SectionType.OpeningThisWeek);
		}

		public void ShowTopBoxOffice(IList<Movie> movies)
		{
			_source.TopBoxMovies = movies;
			ReloadSection(SectionType.TopBoxOffice);
		}

		public void ShowInTheaters(IList<Movie> movies)
		{
			_source.InTheater = movies;
			ReloadSection(SectionType.InTheaters);
		}

		private void ReloadSection(SectionType sectionType)
		{
//			_table.ReloadData();
			_table.ReloadSections(new NSIndexSet((uint)sectionType), UITableViewRowAnimation.None);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_table.Frame = Bounds;
		}
	}
}

