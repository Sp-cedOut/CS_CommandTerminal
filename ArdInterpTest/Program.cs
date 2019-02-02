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
            CommandCollection collect = new CommandCollection();
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
                        if(Spliced[2].ToString() == "r")
                        {
                            int rint=main.GenerateRandomInt(Convert.ToInt32(Spliced[3]), Convert.ToInt32(Spliced[4]));
                            try
                            {
                                vars.ints.Add(Spliced[1], rint.ToString());
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Whoops! {0} already exists!", Spliced[1]);
                            }
                        }
                        else
                        {
                            try
                            {
                                vars.ints.Add(Spliced[1], Spliced[2]);
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Whoops! {0} already exists!", Spliced[1]);
                            }
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
                        collect.CommandCollections.Add(Spliced[1].ToString(), script);
                        //script = main.CreateCommandScript();
                        Console.WriteLine("Created CommandScript instance");
                        break;
                    case "add":
                        //List<string> NewSpliced = main.GetSplicedInput();
                        Spliced.Remove("add");
                        main.AddLineToCommandScript(indexer, Spliced, script);
                        indexer++;
                        break;
                    case "ps":
                        if(collect.CommandCollections.TryGetValue(Spliced[1], out Command value))
                        {
                            main.PrintCommandScript(value);
                        }
                        break;
                    case "runo":
                        main.RunOnce(script, vars, main,script,collect);
                        break;
                    case "runr":
                        int repeat = Convert.ToInt32(Spliced[1]);
                        main.RunRepeat(script, vars, main, repeat,script,collect);
                        break;
                    case "edit":
                        indexer = 0;
                        script = main.EditTryGet(collect,Spliced[1]);
                        Console.WriteLine("Now editing: {0}", Spliced[1]);
                        break;
                    case "ifi":
                        bool ifCheck = main.CompareInts(Spliced[1], Spliced[2], Spliced[3], vars);
                        if (ifCheck)
                        {
                            Console.WriteLine("Output: True");
                        }
                        else Console.WriteLine("Output: False");
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
        GlobalIndexSet indexSet = new GlobalIndexSet();
        public void debug(List<string> inp, Variables vars,SubClass main,Command script,CommandCollection collect)
        {
            int indexer = 0;
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
                    if (inp[2].ToString() == "r")
                    {
                        int rint = main.GenerateRandomInt(Convert.ToInt32(inp[3]), Convert.ToInt32(inp[4]));
                        try
                        {
                            vars.ints.Add(inp[1], rint.ToString());
                        }
                        catch (ArgumentException)
                        {
                            //Console.WriteLine("Whoops! {0} already exists!",inp[1]);
                            vars.ints.Remove(inp[1]);
                            vars.ints.Add(inp[1], rint.ToString());
                            indexSet.indexer++;
                        }
                    }
                    else
                    {
                        try
                        {
                            vars.ints.Add(inp[2], inp[3]);
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Whoops! {0} already exists!", inp[1]);
                        }
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
                case "if":
                    
                    break;
                case "create":
                    script = main.CreateCommandScript();
                    collect.CommandCollections.Add(inp[1].ToString(), script);
                    //script = main.CreateCommandScript();
                    Console.WriteLine("Created CommandScript instance");
                    break;
                case "add":
                    //List<string> NewSpliced = main.GetSplicedInput();
                    inp.Remove("add");
                    main.AddLineToCommandScript(indexer, inp, script);
                    indexer++;
                    break;
                case "ps":
                    if (collect.CommandCollections.TryGetValue(inp[1], out Command value))
                    {
                        main.PrintCommandScript(value);
                    }
                    break;
                case "runo":
                    Command sc = EditTryGet(collect, inp[1]);
                    main.RunOnce(script, vars, main,sc,collect);
                    break;
                case "runr":
                    int repeat = Convert.ToInt32(inp[1]);
                    main.RunRepeat(script, vars, main, repeat,script,collect);
                    break;
                case "edit":
                    script = main.EditTryGet(collect, inp[1]);
                    Console.WriteLine("Now editing: {0}", inp[1].ToString());
                    break;
                case "ifi":
                    bool ifCheck = main.CompareInts(inp[1], inp[2], inp[3], vars);
                    if (ifCheck)
                    {
                        Console.WriteLine("Output: True");
                    }
                    else Console.WriteLine("Output: False");
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
            inp += ";";

            for (int i = 0; i < inp.Length; i++)
            {
                if (inp[i].ToString() == ";")
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
                Console.WriteLine("This variable does not exist in any variable dictionary");
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
                Console.WriteLine("{0}| {1}", kvp.Key, String.Join("~", kvp.Value.ToArray()));
            }
        }
        public void RunOnce(Command scrip, Variables vars, SubClass main,Command script, CommandCollection collect)
        {
            foreach (KeyValuePair<int, List<string>> kvp in script.CommandScript)
            {
                debug(kvp.Value, vars, main,script,collect);
            }
        }
        public void RunRepeat(Command scrip,Variables vars,SubClass main,int repeat, Command script, CommandCollection collect)
        {
            for(int i = 0; i != repeat; i++)
            {
                RunOnce(scrip, vars, main,script,collect);
            }
        }
        public Command EditTryGet(CommandCollection collection, string toGet)
        {
            if (collection.CommandCollections.TryGetValue(toGet, out Command value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("This CommandScript does not exist in CommandCollection.CommandCollections Dictionary");
                return null;
            }
        }
        public int GenerateRandomInt(int r1,int r2)
        {
            Random r = new Random();
            int rand = r.Next(r1, r2);
            return rand;
        }
        public bool CompareInts(string var1,string var2,string comper,Variables vars)
        {
            var var1value = Convert.ToInt32(GetData(var1,vars));
            var var2value = Convert.ToInt32(GetData(var2, vars));
            switch (comper)
            {
                case "==":
                    if (var1value == var2value)
                    {
                        return true;
                    }
                    else return false;
                case ">":
                    if (var1value > var2value)
                    {
                        return true;
                    }
                    else return false;

                case "<":
                    if (var1value < var2value)
                    {
                        return true;
                    }
                    else return false;
                case ">=":
                    if (var1value >= var2value)
                    {
                        return true;
                    }
                    else return false;
                case "<=":
                    if (var1value <= var2value)
                    {
                        return true;
                    }
                    else return false;
                default:
                    Console.WriteLine("Comper: {0} : is not recognized; returning false");
                    return false;
            }
        }
    }
    public class Variables
    {
        public Dictionary<string, string> bools = new Dictionary<string, string>();
        public Dictionary<string, string> ints = new Dictionary<string, string>();
        public Dictionary<string, string> strings = new Dictionary<string, string>();
    } //Dictionaries: bools,ints,strings
    class DataUsage:Variables
    {
        Variables vars = new Variables();
    }//unsued currently
    public class Command
    {
        public Dictionary<int, List<string>> CommandScript = new Dictionary<int, List<string>>();
    }//Dictionary: CommandScript
    public class CommandCollection
    {
        public Dictionary<string, Command> CommandCollections = new Dictionary<string, Command>();
    }//Dictionary: CommandCollections

    public class GlobalIndexSet
    {
        public int indexer = 1;
    }
}
