using System;
using System.Threading;

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

            public int Length
            {
                get => position;
                set => position = value;
            }

            public void Append(string s)
            {
                if (s.Length > buffer.Length - position)
                    throw new ArgumentOutOfRangeException(nameof(s), $"Length of {nameof(s)} exceeds buffer size.");

                for (int i = 0, c = s.Length; i < c; ++i)
                    buffer[position + i] = s[i];

                position += s.Length;
            }

            public void Append(char ch)
            {
                if (position >= buffer.Length)
                    throw new InvalidOperationException("Buffer is full.");

                buffer[position] = ch;
                ++position;
            }

            public bool StartsWith(string s)
            {
                if (s.Length > buffer.Length) return false;

                for (int i = 0, c = s.Length; i < c; ++i)
                {
                    if (buffer[i] != s[i]) return false;
                }

                return true;
            }

            public bool EndsWith(string s)
            {
                if (s.Length > buffer.Length) return false;

                for (int i = 0, c = s.Length; i < c; ++i)
                {
                    if (buffer[position - c + i] != s[i])
                        return false;
                }

                return true;
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

        public class Params
        {
            public string Keyword;
            public bool CanMatchPrefix;
            public bool CanMatchSuffix;
            public int RandomSeed;
            public CancellationToken CancellationToken;
            public Action<string, string> ResultCallback;
        }

        protected readonly string keyword;
        protected readonly bool canMatchPrefix;
        protected readonly bool canMatchSuffix;
        protected readonly Random random;
        protected readonly CancellationToken cancellationToken;
        protected readonly Action<string, string> resultCallback;

        protected long attempts;

        public long Attempts => attempts;

        public string FoundSeed { get; protected set; }
        public string FoundAddress { get; protected set; }

        public Job(Params @params)
        {
            keyword = @params.Keyword;
            canMatchPrefix = @params.CanMatchPrefix;
            canMatchSuffix = @params.CanMatchSuffix;
            random = new Random(@params.RandomSeed);
            cancellationToken = @params.CancellationToken;
            resultCallback = @params.ResultCallback;
            attempts = 0;
        }

        public abstract void Run(object? arg);
    }
}