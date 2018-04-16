using System.IO.MemoryMappedFiles;

namespace PassCypher
{
    internal class MMF
    {
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