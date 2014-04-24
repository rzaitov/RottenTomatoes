using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeView : UIView
	{
		private UITableView _table;
		private BoxOfficeSource _source;

		public BoxOfficeView ()
		{
			BackgroundColor = UIColor.White;

			_table = new UITableView ();
			_table.Source = _source = new BoxOfficeSource ();
			_table.RowHeight = 91f;
			_table.SeparatorInset = UIEdgeInsets.Zero;

			AddSubview (_table);
		}

		public void ShowMovies(IList<Movie> movies)
		{
			_source.Movies = movies;
			_table.ReloadData ();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			_table.Frame = Bounds;
		}
	}
}

