using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Logic;

namespace RottenTomatoes
{
	public class ReviewSource : UITableViewSource
	{
		private const float NullCellHeight = 100f;

		private const string ReuseId = "CriticCell";
		private const string NullCellReuseId = "NullCell";

		CriticReviewCell _calcCell;

		private IList<Review> _reviews;
		public IList<Review> Reviews {
			get {
				return _reviews;
			}
			set {
				Assert.NotNull(value);
				_reviews = value;
			}
		}

		private bool WillShowNullCell {
			get {
				return Reviews.Count == 0;
			}
		}

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
			if (WillShowNullCell) {
				return NullCellHeight;
			} else {
				_calcCell.Bind(Reviews[indexPath.Row]);
				_calcCell.SizeToFit();
				return _calcCell.Frame.Height;
			}
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (WillShowNullCell) {
				UITableViewCell cell = tableView.DequeueReusableCell(NullCellReuseId);
				cell = cell ?? new UITableViewCell(UITableViewCellStyle.Default, NullCellReuseId);

				cell.TextLabel.Text = Strings.NoReviewContent;
				return cell;
			} else {
				CriticReviewCell cell = (CriticReviewCell)tableView.DequeueReusableCell(ReuseId);
				cell = cell ?? new CriticReviewCell(UITableViewCellStyle.Default, ReuseId);

				cell.Bind(Reviews[indexPath.Row]);
				return cell;
			}
		}
	}
}