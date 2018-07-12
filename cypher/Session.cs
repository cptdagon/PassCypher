/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System.IO;
using System.Linq;

namespace PassCypher
{
    internal class Session
    {
        public static void Setsession(string masterkey, string user)
        {
            SessionParameters.Pass = masterkey;
            SessionParameters.Name = user;
            string paths = Path.Combine("Users", user);
            SessionParameters.Keypath = Path.Combine(paths, user + "_Keys.txt");
            string path = Path.Combine(paths, user + "_Config.txt");
            try
            {
                string encryptedstring = File.ReadLines(path).Skip(1).Take(1).First();
                string decryptedstring = StringCipher.RjndlDecrypt(encryptedstring, masterkey);
                SessionParameters.Mail = decryptedstring;
            }
            catch{}
        } 
        public static void Setmode()
        {
            SessionParameters.Mode = Memmappedfile.Read("NetStat");
        }
    }
}
