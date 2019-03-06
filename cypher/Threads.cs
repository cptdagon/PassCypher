/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System.Threading;

namespace PassCypher
{
    internal class Threads
    {
        private Threads() { }
        public static Thread Bootproc { get; set; }
        public static Thread Netproc { get; set; }
    }
}