using System;
using System.Linq;

namespace PassCypher
{
    internal class Format
    {
        public static void Dash()
        {
            string dashes = "";
            for (int i = 0; i < (Console.WindowWidth-1); i++)
            {
                dashes += "-";
            }
            Console.WriteLine(dashes);
            return;
        }
        public static void Space()
        {
            string spaces = "";
            for (int i=0; i < ((Console.WindowWidth-68)/2); i++)
            {
                spaces += " ";
            }
            Console.Write(spaces);
            return;
        }
        public static void Clear()
        {
            Console.Clear();           
            Console.WriteLine();
            Dash();
            NetTest.NetUpdate();
        }
        public static string Dots(int i)
        {
            const string pool = ".";
            var chars = Enumerable.Range(0, i)
                .Select(x => pool[0]);
            return new string(chars.ToArray());
        }
    }
}