using System.Net.Sockets;
using System.Text;
using System;

namespace ChatAppLibrary
{
    public class ChatApp
    {
        public Socket socket = null;
        public ChatApp(Socket sock)
        {
            socket = sock;
        }
        public  void SendMessageForManager()
        {
            while (true)
            {
                SendMessage();
            }
        }

        public  void ReceiveMessageForManager()
        {
            while (true)
            {
                ReceiveMessage();
            }
        }

        public void SendMessage()
        {
            string message = Console.ReadLine();
            byte[] byteSend = Encoding.UTF8.GetBytes(message);
            socket.Send(byteSend);
        }


        public  void ReceiveMessage()
        {
            byte[] bytes = new byte[512];
            int count = socket.Receive(bytes);
            string message = Encoding.UTF8.GetString(bytes, 0, count);
            Console.WriteLine(message);
        }

        public static string ReceiveMessage(Socket socket)
        {
            byte[] bytes = new byte[512];
            int count = socket.Receive(bytes);
            string message = Encoding.UTF8.GetString(bytes, 0, count);
            return message;
        }   

        public static void SendMessage(Socket socket, string message)
        {
            byte[] byteSend = Encoding.UTF8.GetBytes(message);
            socket.Send(byteSend);
        }
    }
}
