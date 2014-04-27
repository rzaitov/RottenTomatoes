using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Logic;

namespace RottenTomatoes
{
	public class ReviewSource : UITableViewSource
	{
		private const string ReuseId = "CriticCell";

		public IList<Review> Reviews { get; set; }

		public ReviewSource()
		{
			Reviews = new Review[]{ };
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return Reviews.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			CriticReviewCell cell = (CriticReviewCell)tableView.DequeueReusableCell(ReuseId);
			cell = cell ?? new CriticReviewCell(UITableViewCellStyle.Default, ReuseId);

			cell.Bind(Reviews[indexPath.Row]);
			return cell;
		}
	}
}