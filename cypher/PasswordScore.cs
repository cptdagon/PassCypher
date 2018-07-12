/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.Text.RegularExpressions;

namespace PassCypher
{
    internal partial class PasswordScore
    {
        public static int Score(string password)
        {
            switch (CheckStrength(password))
            {
                case PassScore.Blank:
                    return 0;
                case PassScore.VeryWeak:
                    Console.ForegroundColor = ConsoleColor.Red;
                    return 1;
                case PassScore.Weak:
                    Console.ForegroundColor = ConsoleColor.Red;
                    return 2;
                case PassScore.Medium:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    return 3;
                case PassScore.Strong:
                    Console.ForegroundColor = ConsoleColor.Green;
                    return 4;
                case PassScore.VeryStrong:
                    Console.ForegroundColor = ConsoleColor.Green;
                    return 5;
                default:
                    break;
            }
            return 0;
        }
        public static PassScore CheckStrength(string password)
        {
            int score = 0;
            if (password.Length < 1)
            {
                return PassScore.Blank;
            }

            if (password.Length < 4)
            {
                return PassScore.VeryWeak;
            }

            if (password.Length >= 8)
            {
                score++;
            }

            if (password.Length >= 12)
            {
                score++;
            }

            if (Regex.Match(password, @"\d+", RegexOptions.ECMAScript).Success)
            {
                score++;
                score += NumericPattern.NumberPattern(password);
            }

            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @".[ ,!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
            {
                score = 2;
            }

            return (PassScore)score;
        }              
    }   
}