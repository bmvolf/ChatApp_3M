using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ChatAppLibrary;

namespace Client_MegaChat
{
    public class ClientMegaChat
    {
        public void Run()
        {
            Socket socket = SetConfigClient();
            SendSettings(socket);
            Thread managerRecive = new Thread(RecieveMessageForManager);
            managerRecive.Start(socket);
            Thread managerSend = new Thread(SendMessageForManager);
            managerSend.Start(socket);
        }
        public Socket SetConfigClient()
        {
            Console.WriteLine("[CLIENT]");
            string ipLineServer = "127.0.0.1";
            int portServer = 7632;
            IPAddress ipAddressServer = IPAddress.Parse(ipLineServer);
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddressServer, portServer);
            Socket socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Нажмите на Enter, чтобы подключиться к серверу!");
            Console.ReadLine();
            socket.Connect(iPEndPoint);
            Console.WriteLine("Клиент подключился к серверу!");
            return socket;
        }
        public void RecieveMessageForManager(object socketParam)
        {
            Socket socket = (Socket)socketParam;
            while (true)
            {
                string message = ChatApp.ReceiveMessage(socket);
                Console.ForegroundColor = takeColor(message.Split('_')[1]);
                Console.WriteLine(message.Split('_')[0]);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public void SendMessageForManager(object socketParam)
        {
            Socket socket = (Socket)socketParam;
            while (true)
            {
                ChatApp.SendMessage(socket, Console.ReadLine());
            }
        }
        private static ConsoleColor takeColor(string color)
        {
            switch (color)
            {
                case "Red":
                    return ConsoleColor.Red;
                case "Green":
                    return ConsoleColor.Green;
                case "Yellow":
                    return ConsoleColor.Yellow;
                case "Blue":
                    return ConsoleColor.Blue;
                case "White":
                    return ConsoleColor.White;
                case "Cyan":
                    return ConsoleColor.Cyan;
                case "Magenta":
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.Gray;
            }
        }
        public static void SendSettings(Socket socket)
        {
            string setting = "";
            Console.Write("Введите ник пользователя: ");
            string name = Console.ReadLine();
            setting += name + "_";
            Console.Write("Теперь введите ваш цвет: ");
            string color = Console.ReadLine();
            setting += color;
            ChatApp.SendMessage(socket, setting);
        }
    }
}
