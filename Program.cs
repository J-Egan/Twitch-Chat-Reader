using System;
using System.Collections.Generic;
using System.Threading;

namespace ChatReader
{
    class Program
    {

        public static List<string> ChannelList = new List<string>();

        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();
            string input = "";
            while (true)
            {
                Console.Clear();
                DisplayCurrentList();
                Console.WriteLine("Enter another channel to track: ");
                input = Console.ReadLine();
                Connection C = new Connection(input);
                Thread t = new Thread(new ThreadStart(C.ReadChat));
                t.IsBackground = true;
                t.Start();

                threads.Add(t);
                ChannelList.Add(input);
                input = "";
            }
        }

        public static void DisplayCurrentList()
        {
            foreach (string s in ChannelList)
            {
                Console.WriteLine("-" + s);
            }
        }
    }
}
