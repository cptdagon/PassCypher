using System;
using System.Linq;
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
                score += NumberPattern(password);
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

        private static int NumberPattern(string password)
        {
            int[] iArray = new int[20];
            int i = 0;
            string numbers = new string(password.Where(c => char.IsDigit(c)).ToArray());

            foreach (char c in numbers)
            {
                iArray[i] = (int)char.GetNumericValue(c);
                i++;
            }

            int j = 1;
            int altres = 0;
            int sumres = 0;
            int sumpos = 0;
            int sumneg = 0;

            foreach (int a in iArray)
            {
                if (j % 2 == 1)
                {
                    altres += a;
                }
                else
                {
                    altres -= a;
                }
                if (a % 2 == 1)
                {
                    sumneg += a;
                }
                else
                {
                    sumpos += a;
                }
                sumres += a;
                j++;
            }

            int N = i;
            int posneg = Math.Abs(sumpos - sumneg);
            int sda = 0;

            try
            {
                sda = (sumres / altres);
            }
            catch (DivideByZeroException)
            {
                sda = (sumres / 1);
            }

            altres = Math.Abs(altres);
            sda = Math.Abs(sda);
            int avg = (sumres / N);

            int score = 0;
            if (altres == 0 || avg == 0)
            {
                score = 2;
            }
            if (posneg == altres)
            {
                score++;
                score++;
            }
            if (posneg == avg)
            {
                score++;
            }
            if (altres == avg)
            {
                score++;
                score++;
            }
            if ((sda * avg) == sumres)
            {
                score++;
            }
            if ((sumpos == 0) || (sumneg == 0))
            {
                if (avg == sda)
                {
                    score++;
                }
            }
            if ((altres + posneg) == (sumpos))
            {
                score++;
            }
            if ((altres + posneg) == (sumneg))
            {
                score++;
            }
            if ((avg + sda) == Math.Ceiling((double)sumres / 2) || (avg + sda) == N)
            {
                score++;
                score++;
            }
            if ((((sumres / posneg) + 1) == sumneg) || (((sumres / posneg) + 1) == sumpos))
            {
                if ((((sumres / posneg) - 1) == sumneg) || (((sumres / posneg) - 1) == sumpos))
                {
                    score++;
                }
            }

            if ((N <= 2) && (score >= 3))
            {
                score = 2;
            }
            if(score >= 3)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }   
}