using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_MegaChat;

namespace ClientFirst_MegaChat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClientMegaChat client_MegaChat = new ClientMegaChat();
            client_MegaChat.Run();
        }
    }
}
