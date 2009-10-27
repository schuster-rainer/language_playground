namespace palindrome
{
	using System.Collections.Generic;
	using System.Linq;
	using Utils;
	class Program
	{
		static void Main(string[] args)
		{
			Diagnostics.Profile (integer_products);
		}

		static int integer_products()
		{
			var threeDigitsPerFactorProducts = ProductFactory.CreateIntegerProducts(100, 999);
			var maxPalindrome = threeDigitsPerFactorProducts.Palindromes().Max();
			return maxPalindrome;
		}

	}

	public static class ProductFactory
	{
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

	public static class PalindromeExtensions
	{
		public static IEnumerable<int> Palindromes(this IEnumerable<int> self)
		{
			foreach (var number in self)
			{
				if (number.ToString().IsPalindrome())
				{
					yield return number;
				}
			}
		}

		public static bool IsPalindrome(this string self)
		{
			int length = self.Length;

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
