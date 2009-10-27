using System;

namespace Utils
{
	using System.Diagnostics;
	using System.Linq.Expressions;

	public static class Diagnostics
	{
		public static void Profile(Func<int> @function)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = @function();

			stopwatch.Stop();

			var stackTrace = new StackTrace(false);
			Console.WriteLine("msec: {0} in {1}",
							   stopwatch.Elapsed.TotalMilliseconds,
							   stackTrace.GetFrame(1).GetMethod());

			Console.WriteLine("Die Antwort auf alle Fragen: {0}",
							   result);
		}

		public static void ProfileExpression(Expression<Func<int>> @function)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = @function.Compile()();

			stopwatch.Stop();

			var stackTrace = new StackTrace(false);
			Console.WriteLine("msec: {0} in {1}",
							   stopwatch.Elapsed.TotalMilliseconds,
							   stackTrace.GetFrame(1).GetMethod());

			Console.WriteLine("Die Antwort auf alle Fragen: {0}",
							   result);
		}
	}
}
