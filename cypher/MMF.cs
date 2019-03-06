/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System.IO.MemoryMappedFiles;

namespace PassCypher
{
    internal class Memmappedfile
    {
        private Memmappedfile() { }
        public static bool Read(string filename)
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(filename))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    accessor.Read(1, out bool boolean);
                    return boolean;
                }
            }
        }
    }
}