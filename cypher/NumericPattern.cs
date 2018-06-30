using System;
using System.Linq;

namespace PassCypher
{
    class NumericPattern
    {
        public static int NumberPattern(string password)
        {
            int[] iArray = new int[20];
            VarInit(out int altres, out int sumres, out int sumpos, out int sumneg, out int i, out int j);

            string numbers = new string(password.Where(c => char.IsDigit(c)).ToArray());

            foreach (char c in numbers)
            {
                iArray[i] = (int)char.GetNumericValue(c);
                i++;
            }

            SumOf(iArray, ref j, ref altres, ref sumres, ref sumpos, ref sumneg);

            int N = i;
            int posneg = Math.Abs(sumpos - sumneg);
            int sda = DivBy0Catch(altres, sumres);

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

            if (score >= 3)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private static int DivBy0Catch(int altres, int sumres)
        {
            int sda = 0;

            try
            {
                sda = (sumres / altres);
            }
            catch (DivideByZeroException)
            {
                sda = (sumres / 1);
            }

            return sda;
        }

        private static void VarInit(out int altres, out int sumres, out int sumpos, out int sumneg, out int i, out int j)
        {
            altres = 0;
            sumres = 0;
            sumpos = 0;
            sumneg = 0;
            i = 0;
            j = 1;
        }

        private static void SumOf(int[] iArray, ref int j, ref int altres, ref int sumres, ref int sumpos, ref int sumneg)
        {
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
        }
    }
}
