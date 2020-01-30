using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace NumbersExtensions
{
    public static class DoubleExtension
    {
        public const int MaxBitsCount = 64;

        public static string BinaryStringRepresentation(this double number)
        {
            Union union = new Union
            {
                Number = number,
            };
            char[] result = ConvertToBinaryString(union.LongNumber);

            return new string(result);
        }

        private static char[] ConvertToBinaryString(long number)
        {
            char[] binaryDoubleString = new char[MaxBitsCount];
            for (int i = 0; i < MaxBitsCount; i++)
            {
                binaryDoubleString[i] = '0';
            }

            byte remainder;
            if (number < 0)
            {
                number = ~number;
                int i = MaxBitsCount - 1;
                while (i >= 0)
                {
                    remainder = (byte)(number % 2);
                    number /= 2;
                    binaryDoubleString[i] = remainder == 1 ? '0' : '1';
                    i--;
                }
            }
            else
            {
                int i = MaxBitsCount - 1;
                while (i >= 0)
                {
                    remainder = (byte)(number % 2);
                    number /= 2;
                    binaryDoubleString[i] = remainder == 1 ? '1' : '0';
                    i--;
                }
            }

            return binaryDoubleString;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            [FieldOffset(0)]
            private double number;
            [FieldOffset(0)]
            private long longNumber;

            public long LongNumber => this.longNumber;

            public double Number
            {
                set => this.number = value;
            }
        }
    }
}
