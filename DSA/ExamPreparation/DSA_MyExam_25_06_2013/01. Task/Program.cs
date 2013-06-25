using System;
using System.Collections.Generic;
using System.Text;


namespace _01.Task
{
    class Program
    {
        static SuperMarker superMarket;
        static StringBuilder result;

        static void Main()
        {
            superMarket = new SuperMarker();
            result = new StringBuilder();
            string line = Console.ReadLine();
            while (line != "End")
            {
                ParseCommand(line);
                line = Console.ReadLine();
            }
            ParseCommand(line);
        }

        static void ParseCommand(string command)
        {
            string[] commandParams = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (commandParams[0][0])
            {
                case 'A':
                    result.AppendLine(superMarket.Append(commandParams[1]));
                    break;
                case 'S':
                    result.AppendLine(superMarket.Serve(int.Parse(commandParams[1])));
                    break;
                case 'I':
                    result.AppendLine(superMarket.Insert(int.Parse(commandParams[1]), commandParams[2]));
                    break;
                case 'F':
                    result.AppendLine(superMarket.Find(commandParams[1]));
                    break;
                case 'E':
                    Console.Write(result);
                    break;
                default:
                    break;
            }
        }
    }

    class SuperMarker
    {
        private StringBuilder result;
        private Dictionary<string, int> FindByName;
        private List<string> queue;

        private Dictionary<int, string> wtf = new Dictionary<int, string>();
        
        private const string OK_MSG = "OK";
        private const string ERR_MSG = "Error";
        private const string ZERO_MSG = "0";

        public SuperMarker()
        {
            
            this.result = new StringBuilder();
            this.FindByName = new Dictionary<string, int>();
            this.queue = new List<string>(1000000);


        }

        public string Append(string name)
        {
            
            this.queue.Add(name);
            if (this.FindByName.ContainsKey(name))
            {
                FindByName[name] = ++FindByName[name];
            }
            else
            {
                FindByName.Add(name, 1);
            }
            return OK_MSG;
        }

        public string Serve(int count)
        {
            if (count > this.queue.Count)
            {
                return ERR_MSG;
            }
            else
            {
                string name = string.Empty;
                for (int i = 0; i < count; i++)
                {
                     name = queue[i];
                   
                    if (FindByName.ContainsKey(name))
                    {                   
                            FindByName[name] = --FindByName[name];
                    }
                    
                    result.Append(name + " ");
                }
                queue.RemoveRange(0, count);
                result.Length--;
                
                string resultt = result.ToString();
                result.Clear();
                return resultt;
            }
        }

        public string Find(string name)
        {
            if (FindByName.ContainsKey(name))
            {
                if (FindByName[name] <= 0)
                {
                    return ZERO_MSG;
                }
                else
                {
                    return FindByName[name].ToString();
                }
            }
            else
            {
                return ZERO_MSG;
            }
        }

        public string Insert(int position, string name)
        {
            if (position > queue.Count || position < 0)
            {
                return ERR_MSG;
            }
            else
            {
                queue.Insert(position, name);
                if (this.FindByName.ContainsKey(name))
                {
                    int count = FindByName[name];
                    count++;
                    FindByName[name] = count;
                    HashSet<int> wtf = new HashSet<int>();
                    
                }
                else
                {
                    FindByName.Add(name, 1);
                }
                return OK_MSG;
            }
        }
    }
    
}