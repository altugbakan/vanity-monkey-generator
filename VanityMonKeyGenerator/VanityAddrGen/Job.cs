using System;

namespace VanityAddrGen
{
    public abstract class Job
    {
        public struct AddressBuffer
        {
            private char[] buffer;
            private int position;

            public AddressBuffer(int size)
            {
                buffer = new char[size];
                position = 0;
            }

            public void Append(char ch)
            {
                if (position >= buffer.Length)
                    throw new InvalidOperationException("Buffer is full.");

                buffer[position] = ch;
                ++position;
            }

            public override string ToString()
            {
                return new string(buffer, 0, position);
            }
        }

        public const string AddressPrefix = "ban_";

        public static readonly char[] NanoBase32Alphabet = "13456789abcdefghijkmnopqrstuwxyz".ToCharArray();

        public static void NanoBase32(ArraySegment<byte> arr, ref AddressBuffer buffer)
        {
            int length = arr.Count;
            int leftover = (length * 8) % 5;
            int offset = leftover == 0 ? 0 : 5 - leftover;

            int value = 0;
            int bits = 0;

            for (int i = 0; i < length; ++i)
            {
                value = (value << 8) | arr[i];
                bits += 8;

                while (bits >= 5)
                {
                    buffer.Append(NanoBase32Alphabet[(value >> (bits + offset - 5)) & 31]);
                    bits -= 5;
                }
            }

            if (bits > 0)
            {
                buffer.Append(NanoBase32Alphabet[(value << (5 - (bits + offset))) & 31]);
            }
        }

        public static void Reverse(ArraySegment<byte> arr)
        {
            Span<byte> tmp = stackalloc byte[arr.Count];
            for (int i = 0, c = arr.Count; i < c; ++i)
            {
                tmp[i] = arr[c - i - 1];
            }
            tmp.CopyTo(arr);
        }
    }
}