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
		private UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.MakeKeyAndVisible ();

			RottenTomatoesService service = new RottenTomatoesService ();
			service.GetTopBoxOfficeAsync (str => {
				Console.WriteLine(str);
			});


			return true;
		}
	}
}