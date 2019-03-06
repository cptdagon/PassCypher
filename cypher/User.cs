/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.IO;

namespace PassCypher
{
    internal class User
    {
        private User() { }
        internal static string[] Fetchusers()
        {
            string[] Directorylist = Directory.GetDirectories("Users");
            Console.WriteLine("Choose a User to login to:");
            Format.Dash();
            Console.WriteLine("Local Users:");
            int i = 0;
            foreach(string User in Directorylist)
            {                
                int position = User.IndexOf("\\", StringComparison.OrdinalIgnoreCase);
                if (position < 0)
                    continue;
                Console.WriteLine("   > " + User.Substring(position + 1));
                Directorylist[i] = User.Substring(position + 1);
                i++;
            }
            Format.Dash();
            Console.WriteLine("   return");
            Format.Dash();
            return Directorylist;            
        }
    }
}