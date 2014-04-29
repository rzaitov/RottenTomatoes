using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
	public static class Assert
	{
		public static void Equals<T>(T first, T second)
		{
			if (!EqualityComparer<T>.Default.Equals(first, second))
				throw new AssertException();
		}

		public static void EqualsAny<T>(T pivot, IEnumerable<T> values)
		{
			if (!values.Contains(pivot))
				throw new AssertException();
		}

		public static void NotNull(object param, string paramName = "")
		{
			if (param == null) {
				throw new AssertException(paramName);
			}
		}

		public static void NotNullAll(params object[] prms)
		{
			foreach (var p in prms)
				NotNull(p);
		}

		public static void NotNullAny(params object[] parameters)
		{
			bool isAnyNotNull = parameters.Any(p => p != null);

			if (!isAnyNotNull) {
				throw new AssertException("all parameters are equal to null");
			}
		}

		public static void True(bool isTrue, string failMessage = "")
		{
			if (!isTrue) {
				throw new AssertException(failMessage);
			}
		}

		public static void False(bool isFalse, string failMessage = "")
		{
			True(!isFalse, failMessage);
		}
	}

	internal class AssertException : Exception
	{
		public AssertException()
		{
		}

		public AssertException(string message)
			: base(message)
		{

		}
	}
}

