/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.Linq;

namespace PassCypher
{
    class NumericPattern
    {
        private static int Score { get; set; }
        private static int Altres { get; set; }
        private static int Sumres { get; set; }
        private static int Sumpos { get; set; }
        private static int Sumneg { get; set; }
        private static int Posneg { get; set; }
        private static int Sumdivaltsum { get; set; }
        private static int Avg { get; set; }

        public static int NumberPattern(string password)
        {
            int[] iArray = new int[20];
            VarInit(out int i, out int j);

            string numbers = new string(password.Where(c => char.IsDigit(c)).ToArray());

            foreach (char c in numbers)
            {
                iArray[i] = (int)char.GetNumericValue(c);
                i++;
            }

            SumOf(iArray, ref j);
            int N = i;        
            DivBy0Catch();
            Absolute();
            Avg = (Sumres / N);
            PatternScore(N);

            if ((N <= 2) && (Score >= 3))
            {
                Score = 2;
            }

            if (Score >= 3)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private static void PatternScore(int N)
        {
            Score = 0;
            if (Altres == 0 || Avg == 0)
            {
                Score = 2;
            }
            if (Posneg == Altres)
            {
                Score++;
                Score++;
            }
            if (Posneg == Avg)
            {
                Score++;
            }
            if (Altres == Avg)
            {
                Score++;
                Score++;
            }
            if ((Sumdivaltsum * Avg) == Sumres)
            {
                Score++;
            }
            if ((Sumpos == 0) || (Sumneg == 0))
            {
                if (Avg == Sumdivaltsum)
                {
                    Score++;
                }
            }
            if ((Altres + Posneg) == (Sumpos))
            {
                Score++;
            }
            if ((Altres + Posneg) == (Sumneg))
            {
                Score++;
            }
            if ((Avg + Sumdivaltsum) == Math.Ceiling((double)Sumres / 2) || (Avg + Sumdivaltsum) == N)
            {
                Score++;
                Score++;
            }
            if (Posneg == 0)
            {
                Score++;
            }
            else
            {
                if ((((Sumres / Posneg) + 1) == Sumneg) || (((Sumres / Posneg) + 1) == Sumpos))
                {
                    if ((((Sumres / Posneg) - 1) == Sumneg) || (((Sumres / Posneg) - 1) == Sumpos))
                    {
                        Score++;
                    }
                }
            }
        }

        private static void Absolute()
        {
            Posneg = Math.Abs(Sumpos - Sumneg);           
            Altres = Math.Abs(Altres);
            Sumdivaltsum = Math.Abs(Sumdivaltsum);
        }

        private static void DivBy0Catch()
        {
            Sumdivaltsum = 0;
            try
            {
                Sumdivaltsum = (Sumres / Altres);
            }
            catch (DivideByZeroException)
            {
                Sumdivaltsum = (Sumres / 1);
            }
        }

        private static void VarInit(out int i, out int j)
        {
            Altres = 0;
            Sumres = 0;
            Sumpos = 0;
            Sumneg = 0;
            Posneg = 0;
            Avg = 0;
            i = 0;
            j = 1;
        }

        private static void SumOf(int[] iArray, ref int j)
        {
            foreach (int a in iArray)
            {
                if (j % 2 == 1)
                {
                    Altres += a;
                }
                else
                {
                    Altres -= a;
                }
                if (a % 2 == 1)
                {
                    Sumneg += a;
                }
                else
                {
                    Sumpos += a;
                }
                Sumres += a;
                j++;
            }
        }
    }
}
