using System;

namespace Utilities
{
	public static class StringAndDateUtilities
	{
		// Convert a string to uppercase
		public static string ToUpperCase(string input)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			return input.ToUpper();
		}

		// Convert a string to lowercase
		public static string ToLowerCase(string input)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));
			return input.ToLower();
		}

		// Capitalize the first letter of a string
		public static string CapitalizeFirstLetter(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return input;
			return char.ToUpper(input[0]) + input.Substring(1).ToLower();
		}

		// Check if a string is a valid date
		public static bool IsValidDate(string dateString, out DateTime date)
		{
			return DateTime.TryParse(dateString, out date);
		}

		// Format a DateTime as a string
		public static string FormatDate(DateTime date, string format = "yyyy-MM-dd")
		{
			return date.ToString(format);
		}

		// Get the difference in days between two dates
		public static int DaysDifference(DateTime startDate, DateTime endDate)
		{
			return (endDate - startDate).Days;
		}

		// Check if a date is in the past
		public static bool IsDateInPast(DateTime date)
		{
			return date < DateTime.Now;
		}

		// Check if a date is in the future
		public static bool IsDateInFuture(DateTime date)
		{
			return date > DateTime.Now;
		}
	}
}
