using System.Runtime.InteropServices;

namespace FranckDuBlosc
{
    public static class DeBlosc
    {
        public unsafe static byte[] Decode(byte[] compressed)
        {
            fixed (byte* compressed_ptr = compressed)
            {

                Blosc2.PInvoke.Blosc.blosc2_cbuffer_sizes((IntPtr)compressed_ptr, out int nbytes, out int cbytes, out int blocksize);

                // Allocate destination buffer using nbytes
                byte[] dest = new byte[nbytes];

                fixed (byte* dest_ptr = dest)
                {
                    // Now decompress
                    int result = Blosc2.PInvoke.Blosc.blosc2_decompress((IntPtr)compressed_ptr, cbytes, (IntPtr)dest_ptr, nbytes);
                }
                return dest;
            }
        }
    }
}