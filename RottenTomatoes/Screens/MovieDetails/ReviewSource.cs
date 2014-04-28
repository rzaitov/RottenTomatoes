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
		private const string NullCellReuseId = "NullCell";

		CriticReviewCell _calcCell;
		public IList<Review> Reviews { get; set; }

		public ReviewSource()
		{
			Reviews = new Review[]{ };

			_calcCell = new CriticReviewCell(UITableViewCellStyle.Default, ReuseId);
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			// if no revies – will display NullCell
			return Math.Max(Reviews.Count, 1);
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (Reviews.Count > 0) {
				_calcCell.Bind(Reviews[indexPath.Row]);
				_calcCell.SizeToFit();

				return _calcCell.Frame.Height;
			}
			else {
				return 100f;
			}
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (Reviews.Count > 0) {
				CriticReviewCell cell = (CriticReviewCell)tableView.DequeueReusableCell(ReuseId);
				cell = cell ?? new CriticReviewCell(UITableViewCellStyle.Default, ReuseId);

				cell.Bind(Reviews[indexPath.Row]);
				return cell;
			}
			else {
				UITableViewCell cell = tableView.DequeueReusableCell(NullCellReuseId);
				cell = cell ?? new UITableViewCell(UITableViewCellStyle.Default, NullCellReuseId);

				cell.TextLabel.Text = Strings.NoReviewContent;
				return cell;
			}
		}
	}
}