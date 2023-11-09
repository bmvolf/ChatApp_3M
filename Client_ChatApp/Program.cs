using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading;
using ChatAppLibrary;
internal class Program
{
    private static Socket socket = null;

    private static void Main(string[] args)
    {
        SetConfigClient();

        // запуск бесконечного получения  сообщений
        Thread threadReceive = new Thread(ReceiveMessageForManager);
        threadReceive.Start();

        // запуск бесконечного отправления сообщения
        Thread threadSend = new Thread(SendMessageForManager);
        threadSend.Start();

        threadReceive.Join();
        threadSend.Join();

        Console.ReadLine();  // pause
    }

    public static void SetConfigClient()
    {
        Console.WriteLine("[CLIENT]");

        string ipLineServer = "127.0.0.1";
        int portServer = 7632;

        IPAddress ipAddressServer = IPAddress.Parse(ipLineServer);

        IPEndPoint iPEndPoint = new IPEndPoint(ipAddressServer, portServer);

        socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        Console.WriteLine("Нажмите на Enter, чтобы подключиться к серверу!");
        Console.ReadLine();

        socket.Connect(iPEndPoint);

        Console.WriteLine("Клиент подключился к серверу!");
    }

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