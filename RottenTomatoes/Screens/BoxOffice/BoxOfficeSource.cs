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

		private IList<Movie> _topBoxMovies;
		public IList<Movie> TopBoxMovies {
			get {
				return _topBoxMovies;
			}
			set {
				Assert.NotNull(value);
				_topBoxMovies = value;
			}
		}

		private IList<Movie> _openingThisWeek;
		public IList<Movie> OpeningThisWeek {
			get {
				return _openingThisWeek;
			}
			set {
				Assert.NotNull(value);
				_openingThisWeek = value;
			}
		}

		private IList<Movie> _inTheater;
		public IList<Movie> InTheater {
			get {
				return _inTheater;
			}
			set {
				Assert.NotNull(value);
				_inTheater = value;
			}
		}

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

