using System;

namespace Chaos.NaCl.Internal.Ed25519Ref10
{
    internal static partial class Ed25519Operations
    {
        public static void crypto_sign_keypair(byte[] pk, int pkoffset, byte[] sk, int skoffset, byte[] seed, int seedoffset)
        {
            GroupElementP3 A;
            int i;

            Array.Copy(seed, seedoffset, sk, skoffset, 32);

            var hasher = Blake2Fast.Blake2b.CreateIncrementalHasher(64);
            hasher.Update(new ArraySegment<byte>(sk, skoffset, 32));
            byte[] h = hasher.Finish();
            //byte[] h = Sha512.Hash(sk, skoffset, 32);//ToDo: Remove alloc
            ScalarOperations.sc_clamp(h, 0);

            GroupOperations.ge_scalarmult_base(out A, h, 0);
            GroupOperations.ge_p3_tobytes(pk, pkoffset, ref A);

            for (i = 0; i < 32; ++i) sk[skoffset + 32 + i] = pk[pkoffset + i];
            CryptoBytes.Wipe(h);
        }

        /// <summary>
        /// Generates public key from secret.
        /// </summary>
        /// <param name="tmp">Temporary array of 64 bytes.</param>
        /// <remarks>This method is added by @alexanderdna to reduce allocation and redundant code.</remarks>
        public static void crypto_public_key(byte[] secret, int secretOffset, byte[] publicKey, int publicKeyOffset, byte[] tmp)
        {
            var hasher = Blake2Fast.Blake2b.CreateIncrementalHasher(64);
            hasher.Update(new ArraySegment<byte>(secret, secretOffset, 32));
            hasher.Finish(tmp);

            GroupElementP3 A;
            ScalarOperations.sc_clamp(tmp, 0);
            GroupOperations.ge_scalarmult_base(out A, tmp, 0);
            GroupOperations.ge_p3_tobytes(publicKey, publicKeyOffset, ref A);
        }
    }
}
