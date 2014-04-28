using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Logic;

namespace RottenTomatoes
{
	public class BoxOfficeSource : UITableViewSource
	{
		private const string ReuseId = "MovieCellReuseId";

		public IList<Movie> TopBoxMovies { get; set; }
		public IList<Movie> OpeningThisWeek { get; set; }
		public IList<Movie> InTheater { get; set; }

		public BoxOfficeSource ()
		{
			TopBoxMovies = OpeningThisWeek = InTheater = new Movie[]{ };
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			var source = GetSourceForSection(section);
			return source.Count;
		}

		public override int NumberOfSections(UITableView tableView)
		{
			return 3;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			MovieCell cell = (MovieCell)tableView.DequeueReusableCell (ReuseId);
			cell = cell ?? new MovieCell (UITableViewCellStyle.Default, ReuseId);

			var source = GetSourceForSection(indexPath.Section);
			cell.Bind(source[indexPath.Row]);

			return cell;
		}

		public override string TitleForHeader(UITableView tableView, int section)
		{
			SectionType type = (SectionType)section;

			switch (type) {
				case SectionType.OpeningThisWeek:
					return Strings.OpeningThisWeek;

				case SectionType.TopBoxOffice:
					return Strings.TopBoxOffice;

				case SectionType.InTheaters:
					return Strings.AlsoInTheaters;

				default:
					throw new NotImplementedException();
			}
		}

		private IList<Movie> GetSourceForSection(int section)
		{
			SectionType type = (SectionType)section;

			switch (type) {
				case SectionType.OpeningThisWeek:
					return OpeningThisWeek;

				case SectionType.TopBoxOffice:
					return TopBoxMovies;

				case SectionType.InTheaters:
					return InTheater;

				default:
					throw new NotImplementedException();
			}
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			IList<Movie> source = GetSourceForSection(indexPath.Section);
			MovieEventArgs arg = new MovieEventArgs {
				Movie = source[indexPath.Row]
			};

			tableView.RaiseEvent("moveClicked", tableView, arg);
		}
	}
}

