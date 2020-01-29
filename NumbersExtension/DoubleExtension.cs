using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NumbersExtensions
{
    public static class DoubleExtension
    {
        public const int MaxBitsCount = 64;

        public static string BinaryStringRepresentation(this double number)
        {
            Union union;
            union.LongNumber = 0;
            union.Number = number;
            string result = Convert.ToString(union.LongNumber, 2);
            int additionalZerosCount = MaxBitsCount - result.Length;
            for (int i = 0; i < additionalZerosCount; i++)
            {
                result = '0' + result;
            }

            return result;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            [FieldOffset(0)]
            public double Number;
            [FieldOffset(0)]
            public long LongNumber;
        }
    }
}
