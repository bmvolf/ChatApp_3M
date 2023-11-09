using System.Net.Sockets;
using System.Text;

namespace ChatAppLibrary
{
    public static class ChatApp
    {
        public static Socket socket = null;

        public static void SendMessageForManager()
        {
            while (true)
            {
                SendMessage();
            }
        }

        public static void ReceiveMessageForManager()
        {
            while (true)
            {
                ReceiveMessage();
            }
        }

        public static void SendMessage()
        {
            string message = Console.ReadLine();

            byte[] byteSend = Encoding.UTF8.GetBytes(message);
            socket.Send(byteSend);
        }


        public static void ReceiveMessage()
        {
            byte[] bytes = new byte[512];
            int count = socket.Receive(bytes);
            string message = Encoding.UTF8.GetString(bytes, 0, count);
            Console.WriteLine(message);
        }
    }
}