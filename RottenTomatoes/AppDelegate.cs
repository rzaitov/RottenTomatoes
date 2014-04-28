using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Logic;

namespace RottenTomatoes
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private UIWindow _window;
		private BoxOfficeViewController _boxOfficeViewController;
		private MovieDetailsViewController _detailsViewController;

		private IRottenTomatoesService _service;
		private UINavigationController _navigationController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			string rottenKey = NSBundle.MainBundle.ObjectForInfoDictionary("RottenTomatoesKey").ToString();
			_service = new RottenTomatoesService (rottenKey);
//			_service = new MockService();

			_window = new UIWindow (UIScreen.MainScreen.Bounds);

			_boxOfficeViewController = new BoxOfficeViewController (_service);
			_detailsViewController = new MovieDetailsViewController(_service);

			_navigationController = new UINavigationController(_boxOfficeViewController);
//			_window.RootViewController = _boxOfficeViewController;
			_window.RootViewController = _navigationController;

			_window.MakeKeyAndVisible ();

			return true;
		}
	}
}