using System;
using System.Collections.Generic;
using System.Globalization;
using static NumbersExtensions.NumbersExtension;

namespace ArrayManipulation
{
    /// <summary>Provides methods for array extension.</summary>
    public static class ArrayExtension
    {
        /// <summary>Maximum value of digit.</summary>
        public const byte MaxValueOfDigit = 9;

        /// <summary>Finds the maxIndex of the balance.</summary>
        /// <param name="array">An array.</param>
        /// <returns>Returns maxIndex or <em>null</em> if maxIndex not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public static int? FindBalanceIndex(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            long rightPartSum = 0;
            for (int i = 2; i < array.Length; i++)
            {
                rightPartSum += array[i];
            }

            long leftPartSum = 0;
            for (int j = 1; j < array.Length - 1; j++)
            {
                leftPartSum += array[j - 1];
                if (leftPartSum == rightPartSum)
                {
                    return j;
                }

                rightPartSum -= array[j + 1];
            }

            return null;
        }

        /// <summary>Finds the maximum item in the given array.</summary>
        /// <param name="array">An array.</param>
        /// <returns>Returns a value of maximum element.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static int FindMaximumItem(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty.");
            }

            return MaximumItem(array, 0, array.Length - 1);
        }

        /// <summary>Creates new array that contains only numbers with given digit.</summary>
        /// <param name="array">An array.</param>
        /// <param name="digit">Filter digit.</param>
        /// <returns>Returns new filtered array.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when array is empty.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <em>digit</em> is out of range.</exception>
        public static int[] FilterArrayByKey(int[] array, byte digit)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} cannot be empty.");
            }

            if (digit > MaxValueOfDigit)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), $"{nameof(digit)} must be a digit");
            }

            var resultArray = new List<int>();
            foreach (int number in array)
            {
                if (HasDigit(number, digit))
                {
                    resultArray.Add(number);
                }
            }

            return resultArray.ToArray();
        }

        /// <summary>Creates new array that contains only palindrome numbers.</summary>
        /// <param name="array">An array.</param>
        /// <returns>Returns new filtered array.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when array is empty.</exception>
        public static int[] FilterArrayByKey(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} cannot be empty.");
            }

            var resultArray = new List<int>();
            foreach (int number in array)
            {
                if (IsPalindrome(number))
                {
                    resultArray.Add(number);
                }
            }

            return resultArray.ToArray();
        }

        private static bool HasDigit(int number, byte digit)
        {
            uint absNumber = (uint)Math.Abs((long)number);
            uint remainder = absNumber;
            while (absNumber > 0 && remainder != digit)
            {
                remainder = absNumber % 10;
                absNumber /= 10;
            }

            return remainder == digit;
        }

        private static int MaximumItem(int[] array, int leftIndex, int rightIndex)
        {
            int middleIndex = (leftIndex + rightIndex) / 2;
            int leftPartMax;
            int rightPartMax;
            if (leftIndex == rightIndex)
            {
                return array[leftIndex];
            }

            leftPartMax = MaximumItem(array, leftIndex, middleIndex);
            rightPartMax = MaximumItem(array, middleIndex + 1, rightIndex);
            return leftPartMax > rightPartMax ? leftPartMax : rightPartMax;
        }
    }
}
