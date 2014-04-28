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

//		private UILabel _calcQuote, _calcCriticPublication;
//		private UIButton _calcMore;
		CriticReviewCell _calcCell;
		public IList<Review> Reviews { get; set; }

		public ReviewSource()
		{
			Reviews = new Review[]{ };

			_calcCell = new CriticReviewCell(UITableViewCellStyle.Default, ReuseId);
//			_calcQuote = UIFactory.MovieDetails.QuoteLabel();
//			_calcCriticPublication = UIFactory.MovieDetails.CriticPublicationLabel();
//			_calcMore = UIFactory.MovieDetails.MoreLinkButton();
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return Reviews.Count;
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			_calcCell.Bind(Reviews[indexPath.Row]);
			_calcCell.SizeToFit();

			return _calcCell.Frame.Height;
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