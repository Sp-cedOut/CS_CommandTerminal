using System;
using System.Collections.Generic;
namespace ArdInterpTest
{
    class MainClass:SubClass
    {


        public static void Main(string[] args)
        {
            MainClass main = new MainClass();

            for(int i = 0; i < 5; i++)
            {
                main.debug();
            }

        }


    }
    class SubClass
    {
        public void debug()
        {
            List<string> Spliced = new List<string>();
            Spliced = GetSplicedInput();
            bool p3;
            //Console.WriteLine("Welcome, type 'help' for method list.");

            if (Spliced[0].ToLower() == "and")
            {
                p3 = ANDcompare(GetBool(Spliced[1]), GetBool(Spliced[2]));
                if (p3) Console.WriteLine("true");
                else Console.Write("false");
            }
        }
        public List<string> GetSplicedInput()
        {
            Console.WriteLine("");
            List<string> SplicedCommand = new List<string>();
            string userinp = Console.ReadLine();
            SplicedCommand = SpliceBySpace(userinp);
            return SplicedCommand;
        }
        public List<string> SpliceBySpace(string inp)
        {
            List<string> Words = new List<string>();
            List<string> Letters = new List<string>();
            //string inp = Console.ReadLine();
            inp += " ";

            for (int i = 0; i < inp.Length; i++)
            {
                if (inp[i].ToString() == " ")
                {
                    string wordToAdd = string.Join("", Letters.ToArray());
                    Words.Add(wordToAdd);
                    Letters.Clear();
                }
                else
                {
                    Letters.Add(inp[i].ToString());
                }
                //(inp[chars])
            }
            return Words;
        }
        public bool ANDcompare(bool p1, bool p2)
        {
            if (p1 && p2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetBool(string p)
        {
            if (p == "0") return false;
            else return true;
        }
    }
}
