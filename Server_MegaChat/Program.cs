using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ChatAppLibrary;

namespace Server_MegaChat
{
    public class User
    {
        public string Name { get; set; }
        public Socket Socket { get; set; }
        public ConsoleColor Color { get; set; }
        public User(string name, Socket socket, ConsoleColor color)
        {
            Name = name;
            Socket = socket;
            Color = color;
        }
    }
    internal class Program
    {
        private static List<User> userList = new List<User>(); //лист юзеров
        static void Main(string[] args)
        {
            Socket serverSocket = SetConfig();
            while(true)
            {
                Socket socket = serverSocket.Accept();
                User user = new User("", socket, ConsoleColor.White);
                Thread thread = new Thread(ManagerSocket);
                thread.Start(user);
                userList.Add(user);
            }
        }
        public static Socket SetConfig()
        {
            Console.WriteLine("[SERVER]");
            string ipLine = "127.0.0.1";
            int port = 7632;
            IPAddress iPAddress = IPAddress.Parse(ipLine);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(iPEndPoint);
            serverSocket.Listen(10);     // больше чем 1
            Console.WriteLine("Сервер запущен! Ожидаем подключение клиента!");
            return serverSocket;
        }
        public static void ManagerSocket(object userParam)
        {
            User user = (User)userParam;
            string settings = ChatApp.ReceiveMessage(user.Socket);
            user.Color = takeColor(settings.Split('_')[1]);
            user.Name = settings.Split('_')[0];
            while (true)
            {
                string message = ChatApp.ReceiveMessage(user.Socket);
                Console.WriteLine($"Принято сообщение от {user.Name}");
                if (message.Contains("command"))
                {
                    string command = message.Split()[1];
                    if (command == "edit")
                    {
                        user.Name = message.Split()[2];
                    }
                }
                foreach (var u in userList)
                {
                    if (u != user)
                    {
                        ChatApp.SendMessage(u.Socket, $"Сообщение от {user.Name}: {message}_{user.Color.ToString()}");
                        Console.WriteLine($"Сообщение отправлено {u.Name}");
                    }
                }
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
    }
}
