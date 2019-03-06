/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

namespace PassCypher
{
    internal class SessionParameters
    {
        private SessionParameters() { }
        public static string Name { get; set; }
        public static string Pass { get; set; }
        public static string Mail { get; set; }
        public static bool Mode { get; set; }
        public static string Keypath { get; set; }
    }
}
