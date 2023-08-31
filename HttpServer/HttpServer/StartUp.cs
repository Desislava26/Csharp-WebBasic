using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    public class StartUp
    {
        static void Main()
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var port = 8080;

            HttpServer server = new HttpServer("127.0.0.1", port);
            server.Start();
        }
    }
}
