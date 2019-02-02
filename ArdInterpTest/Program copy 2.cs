using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
namespace ArdInterpTest
{
    class MainClass:SubClass
    {
        public static void Main(string[] args)
        {
            /*
            Dictionary<string, string> bools = new Dictionary<string, string>();
            Dictionary<string, string> ints = new Dictionary<string, string>();
            Dictionary<string, string> strings = new Dictionary<string, string>();
            */
            MainClass main = new MainClass();
            Variables vars = new Variables();
            Command script = new Command();
            int indexer = 1;

            bool comLoop = true;
            while (comLoop)
            {

                List<string> Spliced = new List<string>();
                Spliced = main.GetSplicedInput();
                bool p3;
                switch (Spliced[0].ToLower())
                {
                    case "and":
                        p3 = main.ANDcompare(main.GetBool(Spliced[1]), main.GetBool(Spliced[2]));
                        if (p3) Console.WriteLine("true");
                        else Console.WriteLine("false");
                        break;
                    case "b":
                        vars.bools.Add(Spliced[1], Spliced[2]);
                        break;
                    case "i":
                        try
                        {
                            vars.ints.Add(Spliced[1], Spliced[2]);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("ArgumentOutOfRangeException: {0}", e);
                        }
                        break;
                    case "s":
                        vars.strings.Add(Spliced[1], Spliced[2]);
                        break;
                    case "pbools":
                        main.PrintBools(vars);
                        break;
                    case "pints":
                        main.PrintInts(vars);
                        break;
                    case "pstrings":
                        main.PrintStrings(vars);
                        break;
                    case "get":
                        Console.WriteLine(main.GetData(Spliced[1],vars));
                        break;
                    case "debug":
                        //main.debug();
                        break;
                    case "end":
                        comLoop = false;
                        break;
                    case "create":
                        script = main.CreateCommandScript();
                        Console.WriteLine("Created CommandScript instance");
                        break;
                    case "add":
                        //List<string> NewSpliced = main.GetSplicedInput();
                        Spliced.Remove("add");
                        main.AddLineToCommandScript(indexer, Spliced, script);
                        indexer++;
                        break;
                    case "ps":
                        main.PrintCommandScript(script);
                        break;
                    case "run":
                        foreach(KeyValuePair<int,List<string>> kvp in script.CommandScript)
                        {
                            main.debug(kvp.Value, vars, main);
                        }
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command: Input String Index[0]");
                        break;
                }
            }
        }
    }
    public class SubClass
    {
        //MainClass main = new MainClass();
        DataUsage dat = new DataUsage();
        public void debug(List<string> inp, Variables vars,SubClass main)
        {
            switch (inp[0].ToLower())
            {
                case "and":
                    bool p3 = main.ANDcompare(main.GetBool(inp[1]), main.GetBool(inp[2]));
                    if (p3) Console.WriteLine("true");
                    else Console.WriteLine("false");
                    break;
                case "b":
                    vars.bools.Add(inp[1], inp[2]);
                    break;
                case "i":
                    try
                    {
                        vars.ints.Add(inp[1], inp[2]);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("ArgumentOutOfRangeException: {0}", e);
                    }
                    break;
                case "s":
                    vars.strings.Add(inp[1], inp[2]);
                    break;
                case "pbools":
                    main.PrintBools(vars);
                    break;
                case "pints":
                    main.PrintInts(vars);
                    break;
                case "pstrings":
                    main.PrintStrings(vars);
                    break;
                case "get":
                    Console.WriteLine(main.GetData(inp[1], vars));
                    break;
                case "end":
                    //comLoop = false;
                    break;
                default:
                    Console.WriteLine("Error: Unknown Command: Input String Index[0]");
                    break;
            }
        }
    
        public List<string> GetSplicedInput()
        {
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
            if (p == "true") return true;
            else return false;
        }
        public void PrintBools(Variables vars)
        {
            foreach (KeyValuePair<string, string> keyValuePair in vars.bools)
            {
                Console.WriteLine("Type = {0}, Key = {1}, Value = {2}", "bool", keyValuePair.Key, keyValuePair.Value);
            }
        }
        public void PrintInts(Variables vars)
        {
            foreach (KeyValuePair<string, string> keyValuePair in vars.ints)
            {
                Console.WriteLine("Type = {0}, Key = {1}, Value = {2}", "int", keyValuePair.Key, keyValuePair.Value);
            }
        }
        public void PrintStrings(Variables vars)
        {
            foreach (KeyValuePair<string, string> keyValuePair in vars.strings)
            {
                Console.WriteLine("Type = {0}, Key = {1}, Value = {2}", "string", keyValuePair.Key, keyValuePair.Value);
            }
        }
        public string GetData(string toGet, Variables vars)
        {
            if (vars.bools.TryGetValue(toGet, out string value))
            {
                Console.WriteLine("Key = {0} , Value = {1}", toGet, value);
                return value;
            }
            else if (vars.ints.TryGetValue(toGet, out value))
            {
                Console.WriteLine("Key = {0} , Value = {1}", toGet, value);
                return value;
            }
            else if (vars.strings.TryGetValue(toGet, out value))
            {
                Console.WriteLine("Key = {0} , Value = {1}", toGet, value);
                return value;
            }
            else
            {
                Console.WriteLine("This variable does not exist in any dictionary");
                return null;
            }
        }
        public Command CreateCommandScript()
        {
            return new Command();
        }
        public void AddLineToCommandScript(int index,List<string> line,Command scrip)
        {
            scrip.CommandScript.Add(index, line);
        }
        public void PrintCommandScript(Command scrip)
        {
            foreach (KeyValuePair<int, List<string>> kvp in scrip.CommandScript)
            {
                Console.WriteLine("{0}| {1}", kvp.Key, (String.Join("; ", kvp.Value.ToArray())));
            }
        }
    }
    public class Variables
    {
        public Dictionary<string, string> bools = new Dictionary<string, string>();
        public Dictionary<string, string> ints = new Dictionary<string, string>();
        public Dictionary<string, string> strings = new Dictionary<string, string>();
    }
    class DataUsage:Variables
    {
        Variables vars = new Variables();
    }
    public class Command
    {
        public Dictionary<int, List<string>> CommandScript = new Dictionary<int, List<string>>();
    }
}
