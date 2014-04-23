﻿using System;
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
			_table = _source = new BoxOfficeSource ();
			AddSubview (_table);
		}

		public void ShowMovies(IList<Movie> movies)
		{
			_source.Movies = movies;
			_table.ReloadData ();
		}
	}
}

