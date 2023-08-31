using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        public HttpServer(string _ipAddress, int _port)
        {
            ipAddress = IPAddress.Parse(_ipAddress);
            port = _port;
            serverListener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            serverListener.Start();

            Console.WriteLine($"Server is listening on {port}");
            Console.WriteLine("Listening for requests");

            while (true)
            {
                var connection = serverListener.AcceptTcpClient();
                var networkStream = connection.GetStream();

                WriteResponce(networkStream, "123456789");
                connection.Close();
            }
        }

        public void WriteResponce(NetworkStream networkStream, string content)
        {
            //string content = "123456789";
            int contentLenght = Encoding.UTF8.GetByteCount(content);
            string response = $@"HTTP/1.1 200 OK
            Content-Type: text/plain; charset=UTF-8
            Content-Length: {contentLenght}

            {content}";
            var responseBytes = Encoding.UTF8.GetBytes(response);
            networkStream.Write(responseBytes, 0, responseBytes.Length);
        }

        private string ReadRequest(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder request = new StringBuilder();
            int totalBytes = 0;

            do
            {

                int bytesRead = networkStream.Read(buffer, totalBytes, buffer.Length);
                totalBytes += bytesRead;

                request.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return request.ToString();
        }
    }
}
