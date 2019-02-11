using System;
using System.Collections.Generic;
using System.Text;

namespace ChatReader
{
    class Processor
    {
        Database db = new Database();
        Queue<string> messageQ = new Queue<string>();

        public Processor()
        {

        }

        public void addMessage(string message)
        {
            messageQ.Enqueue(message);
        }

        public void Run()
        {
            while (true)
            {
                if (messageQ.Count > 0)
                {
                    Process(messageQ.Dequeue());
                }
            }
        }

        private void Process(string message)
        {
            int iCollon = message.IndexOf(":", 1);

            if (iCollon > 0)
            {
                string command = message.Substring(1, iCollon);

                if (command.Contains("PRIVMSG"))
                {
                    var iBang = command.IndexOf("!");
                    if (iBang >= 0)
                    {
                        int channelStart = command.IndexOf("#");
                        int channelEnd = command.IndexOf(" ", channelStart);

                        string channel = command.Substring(channelStart + 1, (channelEnd - channelStart));
                        string speaker = command.Substring(0, iBang);
                        string ChatMessage = message.Substring(iCollon + 1);
                        db.Insert(channel, speaker, ChatMessage);
                    }
                }
            }
        }
    }
}
