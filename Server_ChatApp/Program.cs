using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using ChatAppLibrary;
internal class Program
{
    private static Socket socket = null;  // client socket
    private static void Main(string[] args)
    {
        SetConfig();

        ReceiveMessage();
        Thread threadRecieve = new Thread(RecieveMessageForManager);
        threadRecieve.Start();
        SendMessageForManager();


        Console.ReadLine();
    }

    public static void SetConfig()
    {
        Console.WriteLine("[SERVER]");

        string ipLine = "127.0.0.1";
        int port = 7632;

        IPAddress iPAddress = IPAddress.Parse(ipLine);

        IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

        Socket serverSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        serverSocket.Bind(iPEndPoint);

        serverSocket.Listen(1);     // ptp

        Console.WriteLine("Сервер запущен! Ожидаем подключение клиента!");

        socket = serverSocket.Accept();   // ожидаем подключение - lock

        Console.WriteLine("Клиент подключился!");
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
    public static void RecieveMessageForManager()
    {
        while (true)
        {
            ReceiveMessage();
        }
    }
    public static void SendMessageForManager()
    {
        while (true)
        {
            SendMessage();
        }
    }
}