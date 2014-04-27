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

		private IRottenTomatoesService _service;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
//			_service = new RottenTomatoesService ();
			_service = new MockService();

			_window = new UIWindow (UIScreen.MainScreen.Bounds);

			_boxOfficeViewController = new BoxOfficeViewController (_service);
			_window.RootViewController = _boxOfficeViewController;

			_window.MakeKeyAndVisible ();

//			RottenTomatoesService service = new RottenTomatoesService ();
//			service.GetTopBoxOfficeAsync (movies => {
//				Console.WriteLine(movies);
//			});


			return true;
		}
	}
}