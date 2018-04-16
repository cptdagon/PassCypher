using System.IO;
using System.Linq;

namespace PassCypher
{
    internal class SessionParameters
    {
        public static string Name { get; set; }
        public static string Pass { get; set; }
        public static string Mail { get; set; }
        public static bool Mode { get; set; }
        public static string Keypath { get; set; }
    }
    internal class Session
    {
        public static void Setsession(string masterkey, string user)
        {
            SessionParameters.Pass = masterkey;
            SessionParameters.Name = user;
            string decryptedstring;
            string encryptedstring;
            string path;
            string paths = Path.Combine("Users", user);
            SessionParameters.Keypath = Path.Combine(paths, user + "_Keys.txt");
            path = Path.Combine(paths, user + "_Config.txt");
            try
            {
                encryptedstring = File.ReadLines(path).Skip(1).Take(1).First();
                decryptedstring = StringCipher.RjndlDecrypt(encryptedstring, masterkey);
                SessionParameters.Mail = decryptedstring;
            }
            catch { }
        } 
        public static void Setmode()
        {
            SessionParameters.Mode = MMF.Read("NetStat");
        }
    }
}
