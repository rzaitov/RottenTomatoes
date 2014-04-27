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

		private UILabel _calcQuote;
		public IList<Review> Reviews { get; set; }

		public ReviewSource()
		{
			Reviews = new Review[]{ };

			_calcQuote = UIFactory.MultiLineLabel();
			_calcQuote.Font = Fonts.Italic14;
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return Reviews.Count;
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			_calcQuote.Text = Reviews[indexPath.Row].quote;
			_calcQuote.Begin().Width(280).Commit().SizeToFit();
			return _calcQuote.Frame.Height + 20;
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