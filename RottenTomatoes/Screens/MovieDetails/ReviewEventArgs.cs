using System;
using Logic;

namespace RottenTomatoes
{
	public class ReviewEventArgs : EventArgs
	{
		public Review Review { get ; set; }
	}
}

