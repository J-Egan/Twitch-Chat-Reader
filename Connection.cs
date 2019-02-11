using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatReader
{
    class Connection
    {
        Thread PThread;
        Processor p = new Processor();
        TcpClient tcpClient;
        StreamWriter writer;
        StreamReader reader;
        Database db = new Database();
        string channel = "how248";

        public Connection()
        {
            Reconnect();
        }

        public Connection(string channel)
        {
            this.channel = channel;
            PThread = new Thread(new ThreadStart(p.Run));
            PThread.IsBackground = true;
            PThread.Start();
            Reconnect();
        }

        private void Reconnect()
        {
            tcpClient = new TcpClient("irc.twitch.tv", 6667);
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());

            var username = "how248";
            var password = File.ReadAllText("password.txt");

            writer.WriteLine("PASS " + password + Environment.NewLine +
                "NICK " + username + Environment.NewLine +
                "USER " + username + " 8 * :" + username);
            writer.Flush();

            writer.WriteLine($"JOIN #{channel}");
            writer.Flush();
        }

        public void ReadChat()
        {
            while (true)
            {
                if (!tcpClient.Connected)
                {
                    Reconnect();
                }

                if (tcpClient.Available > 0 || reader.Peek() > 0)
                {
                    string message = reader.ReadLine();
                    p.addMessage(message);
                }
            }
        }
    }
}
