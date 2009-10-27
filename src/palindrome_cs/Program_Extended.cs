using System;
using System.Collections.Generic;
using System.Linq;

namespace palindrome_extended
{
	class Program
	{
		static void Main(string[] args)
		{
			multiplication_products();
			//integer_products();
		}

		private static void integer_products()
		{
			var threeDigitProducts = ProductFactory.CreateIntegerProducts(100, 999);
			var maxPalindrome = threeDigitProducts.Palindromes().Max();
			Console.WriteLine("Produkt: {0}", maxPalindrome);
		}

		private static void multiplication_products()
		{
			var threeDigitProducts = ProductFactory.CreateMultiplicationProducts(100, 999);
			var maxPalindrome = threeDigitProducts.Palindromes().Max();
			Console.WriteLine("Produkt: {0}, Faktor A: {1}, Faktor B: {2}",
				maxPalindrome.Product, maxPalindrome.FactorA, maxPalindrome.FactorB);
		}
	}

	public class Multiplication : IComparable
	{
		public int FactorA { get; private set; }
		public int FactorB { get; private set; }


		public Multiplication(int factorA, int factorB)
		{
			FactorA = factorA;
			FactorB = factorB;
		}

		public int Product
		{
			get { return FactorA * FactorB; }
		}

		public int CompareTo(object obj)
		{
			var mul = obj as Multiplication;
			if (mul == null)
				throw new ArgumentNullException("obj");

			return Product.CompareTo(mul.Product);
		}
	}

	public static class ProductFactory
	{
		public static IEnumerable<Multiplication> CreateMultiplicationProducts(int minFactor, int maxFactor)
		{
			for (int factorA = minFactor; factorA <= maxFactor; factorA++)
			{
				for (int factorB = minFactor; factorB <= maxFactor; factorB++)
				{
					yield return new Multiplication (factorA, factorB);
				}
			}
		}

		public static IEnumerable<int> CreateIntegerProducts(int minFactor, int maxFactor)
		{
			for (int factorA = minFactor; factorA <= maxFactor; factorA++)
			{
				for (int factorB = minFactor; factorB <= maxFactor; factorB++)
				{
					yield return factorA * factorB;
				}
			}
		}
	}

	public static class PlaindromeExtensions
	{
		public static IEnumerable<Multiplication> Palindromes(this IEnumerable<Multiplication> self)
		{
			foreach (var multiplication in self)
			{
				if (multiplication.Product.IsPalindrome())
				{
					yield return multiplication;
				}
			}
		}

		public static IEnumerable<string> Palindromes(this IEnumerable<string> self)
		{
			foreach (var text in self)
			{
				if (text.IsPalindrome())
				{
					yield return text;
				}
			}
		}

		public static IEnumerable<int> Palindromes(this IEnumerable<int> self)
		{
			foreach (var number in self)
			{
				if (number.IsPalindrome())
				{
					yield return number;
				}
			}
		}

		public static bool IsPalindrome(this int self)
		{
			return self.ToString().IsPalindrome();
		}

		public static bool IsPalindrome(this string self)
		{
			int length = self.Length;

			//for equality.
			for (int digit = 0; digit < length; digit++)
			{
				int counterDigit = length - 1 - digit;

				if (self[digit] != self[counterDigit])
					return false;
			}

			return true;
		}
	}
}
