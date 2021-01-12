using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _06_frequency_response
{
    class Program
    {
        static void Main(string[] args)
        {
            //file path
            string path = @"..\..\file";

            //debug counter
            int n=0;
            if (File.Exists(@"..\..\debugcounter_06.txt"))
                n = Convert.ToInt32(File.ReadAllText(@"..\..\debugcounter_06.txt"));
            File.WriteAllText(@"..\..\debugcounter_06.txt",Convert.ToString( n+1));

            //load file
            string file = LoadFile();

            //debug print
            //Console.WriteLine(file);


            //save file to output
            //File.WriteAllText(path + "\\output.txt", file);

            //po4et v2t
            file.CountSplitBy('.', "sentences");

            //pocet slov, po4et vet, nejkrat39 slovo, nej v2ta
            file.CountSplitBy(' ', "words");

            //formating
            string fileclear = file.ClearText();
            fileclear = fileclear.ToLower();

            //debug print
            //Console.WriteLine(fileclear);
            //Console.WriteLine(file);

            //Console.WriteLine();

            //interpunction
            char[] ch = fileclear.ToCharArray();
            Console.WriteLine("all characters: {0}", file.Length);
            Console.WriteLine("characters without interpunction: {0}", ch.Length);
            Console.WriteLine("interpunction: {0} characters", file.ToCharArray().Length- ch.Length);


            //PrintCharArray("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
            //PrintIntArray(CountLetters(file));
            //PrintFR(file,false);
            PrintFR(file, true);

            //Console.WriteLine(CalcTheShift(file));
            //Console.WriteLine(ShiftTheTextByg(file,5));
            //Console.WriteLine(ShiftTheTextByg( ShiftTheTextByg(file, 5),-5));
            
            string shifted= (ShiftTheTextByg(file, 13));
            string correct = CaesarCipher(shifted);
            //Console.WriteLine(correct);

            //char[] chch = file.ToCharArray();
            //string file2 = new string(chch);
            //Console.WriteLine(file2);


            //nubber of specific words
            //Console.WriteLine("Word God: {0}", file.ContainsSpecificWord("god"));
            //Console.WriteLine("Word man: {0}", SpecificWord(words, "man"));
            //Console.WriteLine("Word sin: {0}", SpecificWord(words, "sin"));
            //Console.WriteLine("Word q: {0}", SpecificWord(words, "q"));



            //save output
            File.WriteAllText(path + "\\output.txt", shifted);
            File.WriteAllText(path + "\\output.txt", correct);

            Console.WriteLine("shifted and corrected file saved to file folder");


            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }


        static string LoadFile()
        {
            string path = @"..\..\file";
            Console.Write("Put text file to 'file' folder and write name of the file (enclouding extension)(leave blank for The Bible):");
            string name = Console.ReadLine();
            if (name == "")
                name = "TheBible.txt";
            string fullname = path + @"\" + name;
            if (File.Exists(fullname))
                return File.ReadAllText(fullname);
            else
            {
                Console.WriteLine(" no file '"+name+"' there, try again");
                LoadFile();
            }
            return ("error");
        }

        static void PrintFR(string file, bool order = false)
        {
            int[] fr = file.CountLetters();
            char[] ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            if (!order)
            {
                for (int i = 0; i < ch.Length; i++)
                    Console.WriteLine(ch[i] + "    " + fr[i]);
                Console.WriteLine();
                return;
            }
            //sorting
            for (int i = 0; i < fr.Length; i++)
            {
                int s = i;
                for (int j = i + 1; j < fr.Length; j++)
                    if (fr[j] > fr[s])
                        s = j;
                char q = ch[i];
                int p = fr[i];
                ch[i] = ch[s];
                fr[i] = fr[s];
                ch[s] = q;
                fr[s] = p;

                Console.WriteLine(ch[i] + "    " + fr[i]);
            }
            Console.WriteLine();
            //for (int i = 0; i < fr.Length; i++)
                //Console.Write(ch[i].ToString().ToLower());
            //ETHAONSIRDLUFMWYCGBPVKJZXQ
            //ethaonsirdlufmwycgbpvkjzxq

        }
        static int CalcTheShift(string file, int n = 0)
        {
            
            int[] fr = file.ToLower().CountLetters();
            if (fr.Sum() < 100) { 
                Console.WriteLine("warning less than 100 characters of english alphabet in file, shift is propably wrong");
                Task.Delay(2000).Wait();      
            }
            int[] g = new int[fr.Length];
            for (int i = 0; i < g.Length; i++)
                g[i] = i;
            //sorting
            for (int i = 0; i < fr.Length; i++)
            {
                int s = i;
                for (int j = i + 1; j < fr.Length; j++)
                    if (fr[j] > fr[s])
                        s = j;
                int p = fr[i];
                int r = g[i];
                fr[i] = fr[s];
                g[i] = g[s];
                fr[s] = p;
                g[s] = r;
            }
            //shift with the most frequented letter e
            if (n >= g.Length)
                n -= g.Length;
            return (g[n]-4);
        }
        static string ShiftTheTextByg (string file, int g)
        {
            char []ch = file.ToCharArray();
            //int[] nu=new int[ch.Length];
            char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            char[] alphaupper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();


            for (int j = 0; j < ch.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (ch[j] == alpha[i])
                    {
                        i += g;
                        if (i >= alpha.Length)
                            i -= alpha.Length;
                        if (i < 0)
                            i += alpha.Length;
                        ch[j] = alpha[i];
                        break;
                    }
                    if (ch[j] == alphaupper[i])
                    {
                        i += g;
                        if (i >= alphaupper.Length)
                            i -= alphaupper.Length;
                        if (i < 0)
                            i += alphaupper.Length;
                        ch[j] = alphaupper[i];
                        break;
                    }
                }
            }
            file = new string(ch);
            return (file);

        }

        static string CaesarCipher(string file, int n = 0)
        {
            if (n > 5)
            {
                Console.WriteLine();
                Console.WriteLine("You went through whole alphabet few times, still wont to continue? (Y/N)");
                if (Console.ReadKey().Key == ConsoleKey.N)
                    Environment.Exit(0);
            }
                int g = CalcTheShift(file,n);
            string corrected = ShiftTheTextByg(file, -g);
            //ask if it looks good
            Console.WriteLine(corrected.Substring(0, 300));
            Console.WriteLine();
            Console.WriteLine("does it look good? (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.N)
                corrected=CaesarCipher(file, n + 1);
            Console.WriteLine();
            return (corrected);
        }
    }
    static class StringExtensions
    {
        public static void CountSplitBy(this string a, char split, string objects = "object")
        {
            string[] b = a.Split(split);
            Console.WriteLine(objects + ":{0}", b.Length);
        }
        public static void PrintCharArray(this char[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + "    ");
            Console.WriteLine();
        }
        public static void PrintIntArray(this int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + "      ");
            Console.WriteLine();
        }
        //not a good method
        public static string ClearTextSlow(this string a)
        {
            char[] ch = a.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (".:,0123456789".Contains(ch[i]))
                {
                    for (int j = i; j < ch.Length - 1; j++)
                        ch[j] = ch[j + 1];
                    Array.Resize<char>(ref ch, ch.Length - 1);
                    i--;
                }
            }
            a = new string(ch);
            return a;
        }

        public static string ClearText(this string a)
        {
            char[] ch = a.ToCharArray();
            List<string> b = new List<string>();

            for (int i = 0; i < ch.Length; i++)
                if (!".:,0123456789".Contains(ch[i]))
                    b.Add(ch[i].ToString());
            a = string.Join("", b);
            return a;
        }

        public static int ContainsSpecificWord(this string file, string word)
        {
            string []items = file.Split(' ');
            int ii = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Contains(word))
                {
                    ii++;
                    //Console.WriteLine(items[i]);
                }
            }
            return (ii);
        }
        //frequency of letters
        public static int[] CountLetters(this string file)
        {
            char[] ch = file.ToLower().ToCharArray();
            char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            int[] fr = new int[alpha.Length];
            for (int i = 0; i < alpha.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < ch.Length; j++)
                {
                    if (ch[j] == alpha[i])
                        k++;
                }
                fr[i] = k;
            }
            return fr;
        }
    }
}
