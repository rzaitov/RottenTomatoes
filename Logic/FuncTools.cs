using System;

namespace Logic
{
	public static class FuncTools
	{
		public static void SafeInvoke(Action x)
		{
			if (x != null)
				x();
		}

		public static void SafeInvoke<A>(Action<A> x, A a)
		{
			if (x != null)
				x(a);
		}

		public static void SafeInvoke<A, B>(Action<A, B> x, A a, B b)
		{
			if (x != null)
				x(a, b);
		}

		public static void SafeInvoke(EventHandler handler, object sender, EventArgs args)
		{
			if (handler != null)
				handler(sender, args);
		}

		public static void SafeInvoke<T>(EventHandler<T> handler, object sender, T args) where T : EventArgs
		{
			if (handler != null)
				handler(sender, args);
		}
	}
}