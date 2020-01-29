using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using static ArrayManipulation.ArrayExtension;
using static Day04.RandomArrays;

namespace ArrayManipulation.Tests
{
    #region ArrayExtensionTestsDataSource

    public class ArrayExtensionTestsDataSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return GetOrderedArray(0, 10_000_000);
            yield return GetOrderedArray(0, 100_000_000);
        }
    }

    #endregion

    [TestFixture]
    public class ArrayExtensionTests
    {
        #region FindBalanceIndexTests

        [Test]
        public void FindBalanceIndex_ArrayIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FindBalanceIndex(null));
        }

        [TestCase(new int[] { }, ExpectedResult = null)]
        [TestCase(new int[] { 1 }, ExpectedResult = null)]
        [TestCase(new int[] { 1, 2, 1 }, ExpectedResult = 1)]
        [TestCase(new int[] { -1, 2, 3, 1 }, ExpectedResult = 2)]
        [TestCase(new int[] { 0, 0, 0, 0, 0, 0 }, ExpectedResult = 1)]
        [TestCase(new int[] { 2, 0, 5, 14, 3 }, ExpectedResult = null)]
        [TestCase(new int[] { 1, 2, 1, 50000 }, ExpectedResult = null)]
        [TestCase(new int[] { 100, -1, 50, -1, 100 }, ExpectedResult = 2)]
        [TestCase(new int[] { 1, 2, 5, 8, 0, 2, 4, 6, 4 }, ExpectedResult = 4)]
        [TestCase(new int[] { -1, 1, -1, 1, -1, 1, 32 }, ExpectedResult = null)]
        [TestCase(new int[] { -32, 8, 1, 2, 4, 5, 12, -3, 0 }, ExpectedResult = 7)]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 5)]
        [TestCase(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, ExpectedResult = null)]
        [TestCase(new int[] { int.MinValue, int.MinValue, 50, int.MinValue, int.MinValue }, ExpectedResult = 2)]
        public int? FindBalanceIndexTests(int[] array)
        {
            return FindBalanceIndex(array);
        }

        #endregion

        #region FindMaximumItemTests

        [Test]
        public void FindMaximumItem_ArrayIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>((() => FindMaximumItem(null)));
        }

        [Test]
        public void FindMaximumItem_ArrayIsEmpty_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>((() => FindMaximumItem(new int[] { })));
        }

        [TestCase(new int[] { 1 }, ExpectedResult = 1)]
        [TestCase(new int[] {2, 0, 5, 14, 3}, ExpectedResult = 14)]
        [TestCase(new int[] {0, 0, 0, 0, 0, 0}, ExpectedResult = 0)]
        [TestCase(new int[] {-1, 1, -1, 1, -1, 1, 32}, ExpectedResult = 32)]
        [TestCase(new int[] { 1, 2, 5, 8, 0, 2, 4, 6, 4 }, ExpectedResult = 8)]
        [TestCase(new int[] { -32, 8, 1, 2, 4, 5, 12, -3, 0 }, ExpectedResult = 12)]
        public int FindMaximumItemTests(int[] array)
        {
            return FindMaximumItem(array);
        }

        [Order(1)]
        [TestCaseSource(typeof(ArrayExtensionTestsDataSource))]
        public void FindMaximumItem_BigArraysTests(int[] array)
        {
            int expected = array[array.Length - 1];

            int actual = FindMaximumItem(array);

            Assert.That(actual == expected);
        }
        #endregion

        #region FilterArrayByKeyTests

        [Test]
        public void FilterArrayByKey_Digit_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => FilterArrayByKey(null, 2));

        [Test]
        public void FilterArrayByKey_Digit_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => FilterArrayByKey(new int[0], 7));

        [Test]
        public void FilterArrayByKey_Palindrome_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => FilterArrayByKey(null));

        [Test]
        public void FilterArrayByKey_Palindrome_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => FilterArrayByKey(new int[0]));

        [Test]
        public void FilterArrayByKey_Digit_DigitIs17_ThrowArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => FilterArrayByKey(new int[] { 1, 2, 45, 14 }, 17));

        [TestCase(new[] { 7, 2, 5, 5, -1, -1, 2 }, 9, ExpectedResult = new int[0])]
        [TestCase(new[] { 2212332, 1405644, -1236674 }, 0, ExpectedResult = new[] { 1405644 })]
        [TestCase(new[] { 53, 71, -24, 1001, 32, 1005 }, 2, ExpectedResult = new[] { -24, 32 })]
        [TestCase(new[] { 27, 102, 15, 0, 34, 0, 0 }, 0, ExpectedResult = new[] { 102, 0, 0, 0 })]
        [TestCase(new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, 7, ExpectedResult = new[] { 7, 7, 70, 17 })]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, ExpectedResult = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(new[] { int.MinValue, int.MaxValue, int.MinValue, 104123531, 36, 0 }, 5, ExpectedResult = new[] { 104123531 })]
        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, 7, ExpectedResult = new[] { -27, 173, 371132, 7556, 7243, 10017 })]
        public int[] FilterArrayByKey_Digit_WithAllValidParameters(int[] array, byte digit)
        {
            return FilterArrayByKey(array, digit);
        }

        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, ExpectedResult = new int[0])]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, ExpectedResult = new[] { 7, 1, 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { int.MinValue, int.MaxValue, 101020101, int.MinValue, 104123531, 36, 0, 123321 }, ExpectedResult = new[] { 101020101, 0, 123321 })]
        public int[] FilterArrayByKey_Palindrome_Tests(int[] array)
        {
            return FilterArrayByKey(array);
        }

        [Test]
        public void FilterArrayByKey_Palindrome_BigArray_EmptyArray()
        {
            int[] expected = new int[0];

            int[] actual = FilterArrayByKey(GetOneValueArray(13, 100_000_000));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterArrayByKey_Palindrome_BigArray()
        {
            int[] expected = GetOneValueArray(121, 100_000_000);

            int[] actual = FilterArrayByKey(expected);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case1()
        {
            int[] expected = NumbersWithDigit(2, 100_000_000);

            int[] actual = FilterArrayByKey(expected, 2);

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case2()
        {
            int[] expected = new int[0];
            
            int[] actual = FilterArrayByKey(GetOneValueArray(26, 100_000_000), 8);
                           
            Assert.AreEqual(actual, expected);
        }

        #endregion
    }
}