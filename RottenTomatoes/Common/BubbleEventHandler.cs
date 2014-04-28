using System;

namespace RottenTomatoes
{
	public class BubbleEventHandlerAttribute : Attribute
	{
		public string EventType { get; private set; }

		public BubbleEventHandlerAttribute(string eventType)
		{
			EventType = eventType;
		}
	}
}