using System;
using System.Reflection;

using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace RottenTomatoes
{
	public static class ResponderHelper
	{
		public static void RaiseEvent(this UIResponder responder, string eventType, params object[] eventParameters)
		{
			MethodInfo mi = null;
			while (responder != null && !IsBubbleEventHandler(responder, eventType, out mi))
				responder = responder.NextResponder;

			if (mi == null)
				return;

			mi.Invoke(responder, eventParameters);
		}

		private static bool IsBubbleEventHandler(UIResponder responder, string eventType, out MethodInfo handler)
		{
			handler = null;

			Type type = responder.GetType();
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var m in methods)
			{
				BubbleEventHandlerAttribute attr = m.GetCustomAttribute<BubbleEventHandlerAttribute>();

				if (attr == null)
					continue;

				if (attr.EventType == eventType) {
					handler = m;
					return true;
				}
			}

			return false;
		}
	}
}

