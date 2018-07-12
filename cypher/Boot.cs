using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace PassCypher
{
    internal class Boot
    {
        public static void BootSetup()
        {
            Console.SetWindowSize(70, 40);
            BootSequence.Sequence = new AutoResetEvent(false);
            Threads.Bootproc = new Thread(BootSequence.Main)
            {
                IsBackground = true
            };
            Threads.Bootproc.Start();

            //int t = 0;
            while (!BootSequence.Sequence.WaitOne()) { }
            
            Format.Clear();
            Console.WriteLine();
            for (int i = 0; i < 31; i++)
            {
                Format.Space();
                Console.WriteLine(Graphic(i));
                Thread.Sleep(50);
            }
            Thread.Sleep(150);
            ApplicaitonContext();
            Thread.Sleep(2000);
            Format.Clear();
            return;
        }

        private static void ApplicaitonContext()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Format.Dash();
            Console.WriteLine("         {0} {1} © {2} {3}\n",
                                fileVersionInfo.ProductName,
                                fileVersionInfo.ProductVersion,
                                fileVersionInfo.CompanyName,
                                DateTime.Now.Year.ToString(CultureInfo.CreateSpecificCulture("en-GB")));
            Format.Dash();
        }

        private static string Graphic(int i)
        {
            string[] ASCII = {  "                        `-/osyyyyyyyyyys+/-`                        ",
                                "                   `:oyhy+:-`          `-/oyhy+-                    ",
                                "                `+hho-                       `-ohy/`                ",
                                "              :hh/`                              `+hy:              ",
                                "            +do.      `-:/+++//:-.`                 .sd/            ",
                                "          /do`    -odNMMMMMMMMMMMMMNm/                `sd:          ",
                                "        `hh.    /mMMMMmyo/:::/+oydNMM+                  .dy`        ",
                                "       -m+    `hMMMMs.             `-.                    om.       ",
                                "      :N:    `mMMMN:                                       /N-      ",
                                "     -N:     hMMMM:                                         /N.     ",
                                "    `m+     :MMMMy           `:::::::::::--.`                od`    ",
                                "    sd      sMMMM:           -MMMMMMMMMMMMMMMMNds:            m+    ", 
                                "   `N:      yMMMM.           `++++++++ooosyhdMMMMMNo`         +m    ", 
                                "   /N       yMMMM.                            :yMMMMN:        `M:   ",
                                "   sh       +MMMM+                              -mMMMM/        m+   ",
                                "   yy       `NMMMm`                              .NMMMN.       do   ",
                                "   sh        /MMMMh`                              +MMMMo       m+   ",
                                "   /N`        +MMMMm/               ``            .MMMMh      `M-   ",
                                "   `N:         :mMMMMms:.      `-/sdMo            `MMMMd      +m    ",
                                "    sd           /hMMMMMMMNNNNMMMMMMN/            .MMMMy     `m+    ",
                                "    `m+            `:+yhdmmmmmdhyo/.              oMMMM:     sd     ",
                                "     -N:                                         .NMMMh     /m.     ",
                                "      :N:                                       -mMMMd`    +m-      ",
                                "       -mo                                    .sMMMMy`    sd.       ",
                                "        `yh.                 `++++++++++++oshmMMMMh-    -ds`        ",
                                "          :ds`               -MMMMMMMMMMMMMMMMmy+`    .yd-          ",
                                "            /ds.             `::::::::::::-.`       -yh:            ",
                                "              :yh+`                              .+hy-              ",
                                "                `/yho:`                      `:shy:`                ",
                                "                    -+yhyo/:.`        `.:/oyhy+.                    ",
                                "                         .:+osyyyyyyyyso+:.                         "};
            string line = ASCII[i];
            return (line);
        }
    }
}
