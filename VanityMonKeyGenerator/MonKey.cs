﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Blake2Fast;
using Chaos.NaCl.Internal.Ed25519Ref10;
using VanityAddrGen;

namespace VanityMonKeyGenerator
{
    public class MonKey
    {
        public string Address;
        public string Seed;
        public string Svg;
        public MonKey()
        {
            Job.AddressBuffer addressBuffer = new Job.AddressBuffer(Job.AddressPrefix.Length + 60);

            byte[] seedBytes = new byte[32];
            byte[] secretBytes = new byte[32];
            byte[] indexBytes = new byte[4];
            byte[] publicKeyBytes = new byte[32];
            byte[] checksumBytes = new byte[5];
            byte[] tmp = new byte[64];

            Random random = new Random();
            random.NextBytes(seedBytes);

            var hasher = Blake2b.CreateIncrementalHasher(32);
            hasher.Update(seedBytes);
            hasher.Update(indexBytes);
            hasher.Finish(secretBytes);
            Ed25519Operations.crypto_public_key(secretBytes, 0, publicKeyBytes, 0, tmp);

            Blake2b.ComputeAndWriteHash(5, publicKeyBytes, checksumBytes);
            Job.Reverse(checksumBytes);

            Job.NanoBase32(publicKeyBytes, ref addressBuffer);
            Job.NanoBase32(checksumBytes, ref addressBuffer);

            Address = "ban_" + addressBuffer.ToString();
            Seed = ByteArrayToHexString(seedBytes);
        }
        public async Task<string> RequestMonKey()
        {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync("https://monkey.banano.cc/api/v1/monkey/" + Address);
            client.Dispose();
            return response;
        }

        public string RequestMonKeySync()
        {
            RequestMonKey().Wait();
            return RequestMonKey().Result;
        }

        private string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }
    }
}
