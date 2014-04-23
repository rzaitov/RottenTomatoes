﻿using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeSource : UITableViewSource
	{
		private const string ReuseId = "MovieCellReuseId";

		public IList<Movie> Movies { get; set; }

		public BoxOfficeSource ()
		{
			Movies = new Movie[]{ };
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return Movies.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			MovieCell cell = (MovieCell)tableView.DequeueReusableCell (ReuseId);
			cell = cell ?? new MovieCell (UITableViewCellStyle.Default, ReuseId);

			cell.Bind (Movies [indexPath.Row]);

			return cell;
		}
	}
}

