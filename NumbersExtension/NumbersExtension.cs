using System;
using System.Globalization;

namespace NumbersExtensions
{
    /// <summary>Provides number extensions methods.</summary>
    public static class NumbersExtension
    {
        /// <summary>Minimum bit number.</summary>
        public const int MinBitIndex = 0;

        /// <summary>Maximum bit number (32-bit numbers).</summary>
        public const int MaxBitIndex = 31;

        public static readonly AppSettings AppSetting;

        static NumbersExtension()
        {
            AppSetting = new AppSettings
            {
                Epsilon = 0.1,
            };
        }

        /// <summary>Inserts the number into another.</summary>
        /// <param name="numberSource">The source number.</param>
        /// <param name="numberIn">Number to insert.</param>
        /// <param name="rightIndex">Right position.</param>
        /// <param name="leftIndex">Left position.</param>
        /// <returns>Returns new number.</returns>
        /// <exception cref="ArgumentException">Thrown when rightIndex index is greater than leftIndex index.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when rightIndex index or leftIndex index are out of range.</exception>
        public static int InsertNumberIntoAnother(int numberSource, int numberIn, int rightIndex, int leftIndex)
        {
            if (rightIndex > leftIndex)
            {
                throw new ArgumentException($"Invalid arguments.");
            }

            if (rightIndex > MaxBitIndex || rightIndex < MinBitIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(rightIndex), "Invalid value.");
            }

            if (leftIndex > MaxBitIndex || leftIndex < MinBitIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(leftIndex), "Invalid value.");
            }

            if (rightIndex == MinBitIndex && leftIndex == MaxBitIndex)
            {
                return numberIn;
            }

            int mask = int.MaxValue;

            if (leftIndex == MaxBitIndex)
            {
                numberSource &= mask;
            }

            int numberSourceCopy = numberSource;
            numberSource >>= leftIndex + 1;
            numberSource <<= leftIndex + 1;
            int multiplierForZeroingLeftPart = mask >> (MaxBitIndex - rightIndex);
            numberSourceCopy &= multiplierForZeroingLeftPart;
            numberSource |= numberSourceCopy;
            multiplierForZeroingLeftPart = mask >> (MaxBitIndex - (leftIndex - rightIndex + 1));
            numberIn &= multiplierForZeroingLeftPart;
            numberIn <<= rightIndex;
            return numberSource | numberIn;
        }

        /// <summary>Determines whether the specified number is palindrome.</summary>
        /// <param name="number">Number.</param>
        /// <returns>
        ///   <c>true</c> if the specified number is palindrome; otherwise, <c>false</c>.</returns>
        public static bool IsPalindrome(int number)
        {
            if (number < 0)
            {
                return false;
            }

            string numberString = Convert.ToString(number, CultureInfo.InvariantCulture);
            int firstDigitIndex = 0;
            int lastDigitIndex = numberString.Length - 1;
            return IsPalindromeString(numberString, firstDigitIndex, lastDigitIndex);
        }

        /// <summary>Finds the Nth root.</summary>
        /// <param name="number">The number.</param>
        /// <param name="power">Root index.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>Returns Nth root of given number.</returns>
        /// <exception cref="ArgumentException">Trown when <em>n </em>is less or equals zero<em>, accuracy </em>is out of range.</exception>
        /// <exception cref="ArithmeticException">Thrown when root index <em>n</em> is even and <em>number</em> is less than zero.</exception>
        public static double FindNthRoot(double number, int power, double accuracy)
        {
            if (number < 0 && power % 2 == 0)
            {
                throw new ArithmeticException($"Number should be greater or equals zero if {power} is even.");
            }

            if (power <= 0)
            {
                throw new ArgumentException($"{nameof(power)} should be positive.");
            }

            if (accuracy < 0 || accuracy > AppSetting.Epsilon)
            {
                throw new ArgumentException($"{nameof(accuracy)} should be in the range [0; {AppSetting.Epsilon}].");
            }

            if (number == 0.0)
            {
                return 0.0;
            }

            double current = number / 2;
            double next = (1.0 / power) * (((power - 1) * current) + (number / Math.Pow(current, power - 1)));
            bool hasFound = false;
            while (!hasFound)
            {
                next = (1.0 / power) * (((power - 1) * current) + (number / Math.Pow(current, power - 1)));
                if (Math.Abs(current - next) < accuracy)
                {
                    break;
                }

                current = next;
            }

            return current;
        }

        private static bool IsPalindromeString(string numberString, int leftIndex, int rightIndex)
        {
            return leftIndex >= rightIndex ?
                true : numberString[leftIndex] != numberString[rightIndex] ?
                    false : IsPalindromeString(numberString, leftIndex + 1, rightIndex - 1);
        }
    }
}