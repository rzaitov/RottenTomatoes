using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace RottenTomatoes
{
	public class WebViewController : UIViewController
	{
		private UIWebView _webView;

		public string ReviewUrl { get; set;}

		public override void LoadView()
		{
			base.LoadView();
			_webView = new UIWebView();

			View = _webView;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NSUrl url = new NSUrl(ReviewUrl);
			NSUrlRequest request = new NSUrlRequest(url);
			_webView.LoadRequest(request);
		}
	}
}

