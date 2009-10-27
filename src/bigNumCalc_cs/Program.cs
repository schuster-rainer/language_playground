namespace bigNumCalc
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using Utils;

	class Program
	{
		public static readonly string BigNumString = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

		static void Main(string[] args)
		{
			sum_fast();
			sum_fast_extension_method();
			sum_linq_extension_methods();
		}

		private static void sum_linq_extension_methods()
		{
			Diagnostics.Profile (() => BigNumString.AdjoiningProduct (5).Max());
		}

		private static void sum_fast_extension_method()
		{
			Diagnostics.Profile(() => BigNumString.AdjoiningProductFast(5).Max());
		}

		private static void sum_fast()
		{
			Diagnostics.Profile(() =>
			         	{
							var aggregator = new DigitReducer(5, BigNumString);
							//aggregator.Initial = 0;
							//aggregator.Reduce = (state, next) => state + next;
			         		return aggregator.Max();
			         	}
				);

			
		}

		private static void product_fast ()
		{
			Diagnostics.Profile(() =>
			         	{
			         		var reducer = new DigitReducer (5, BigNumString);
			         		reducer.Initial = 1;
							reducer.Reduce = (state, next) => state * next;
			         		return reducer.Max();
			         	});
		}

		public class DigitReducer : IEnumerable<int>
		{
			private readonly string inputStream;
			private readonly int numDigits;

			public Func <int, int, int> Reduce;
			public int Initial { get; set; }

			public DigitReducer(int numDigits, string inputStream)
			{
				this.inputStream = inputStream;
				this.numDigits = numDigits;

				//default the reducer to a sum strategy
				Initial = 0;
				Reduce = (state, next) => state + next;
			}

			public IEnumerator<int> GetEnumerator()
			{
			    int length = inputStream.Length;

				//Func<int, int> reduceNumDigitsFromIndex =
				//    idx =>
				//    {
				//        //int result = Initial;
				//        int result = 0;
				//        //for (int addCount = 0; addCount < numDigits && index + addCount < length; addCount++)
				//        for (int addCount = 0; addCount < numDigits; addCount++)
				//        {
				//            //result = Reduce(result, this.inputStream[index + addCount] - '0');
				//            result = result + this.inputStream[idx + addCount] - '0';
				//        }
				//        return result;
				//    };

			    for (int idx = 0; idx < length-numDigits; idx++)
			    {
			        //yield return reduceNumDigitsFromIndex(idx);

					int result = 0;
					for (int addCount = 0; addCount < numDigits; addCount++)
					{
						//result = Reduce(result, this.inputStream[index + addCount] - '0');
						result = result + this.inputStream[idx + addCount] - '0';
					}

			    	yield return result;
			    }
			}

			IEnumerator IEnumerable.GetEnumerator ()
			{
				return GetEnumerator();
			}
		}
	}

	public static class NumberExtensions
	{
		public static IEnumerable<int> AdjoiningProduct(this string self, int numDigits)
		{
			int count = self.Length;
			for (int idx = 0; idx < count; idx++)
			{
				yield return self.Select(digit => digit - '0').Skip(idx).Take(numDigits).Aggregate(
						(aggregatedValue, next) => aggregatedValue + next);
			}
		}

		public static IEnumerable<int> AdjoiningProductFast(this string self, int numDigits)
		{
		    int length = self.Length;

			//Func<int, int> takeNumDigitsAndAggregate =
			//    idx =>
			//    {
			//        int result = 0;
			//        //for (int addCount = 0; addCount < numDigits && index + addCount < length; addCount++)
			//        for (int count = 0; count < numDigits; count++)
			//        {
			//            result = result + (self[idx + count] - '0');
			//        }
			//        return result;
			//    };

			for (int idx = 0; idx < length-numDigits; idx++)
		    {
		        //yield return takeNumDigitsAndAggregate(idx);
				
				int result = 0;
				//for (int addCount = 0; addCount < numDigits && index + addCount < length; addCount++)
				for (int count = 0; count < numDigits; count++)
				{
					result = result + (self[idx + count] - '0');
				}

		    	yield return result;
		    }
		}
	}
}
