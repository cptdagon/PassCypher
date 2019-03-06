/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.Diagnostics;
using System.Reflection;

namespace PassCypher
{
    internal static class Help
    {
        public static void GetHelp()
        {
            Format.Clear();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = typeof(AssemblyDescriptionAttribute);
            AssemblyDescriptionAttribute attribute = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, type);
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Format.Dash();
        }
    }
}
