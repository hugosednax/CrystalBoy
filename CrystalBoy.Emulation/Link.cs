using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace CrystalBoy.Emulation
{
    public class Link : MarshalByRefObject
    {
        /*
        private const int PORT = 10001;

        GameBoyMemoryBus bus;
        string path = @"..\..\..\output\";
        FileStream fileOpcodeStream;
        string ip = "120.120.120.120"; // hardcoded IP
        Socket serverSocket, clientSocket;
        byte[] byteDataReceive = new byte[1];
        byte[] byteDataSend = new byte[1];
        Socket sending_socket;
        IPAddress send_to_address;
        IPEndPoint sending_end_point;


        public Link(GameBoyMemoryBus bus)
        {
            this.bus = bus;
            fileOpcodeStream = File.Create(path + "link.txt");
            serverSocketActivation();
            sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                            ProtocolType.Udp);
            send_to_address = IPAddress.Parse(this.ip);
            sending_end_point = new IPEndPoint(send_to_address, PORT);
        }
        public Link(GameBoyMemoryBus bus, string ip)
        {
            this.bus = bus;
            this.ip = ip;
            fileOpcodeStream = File.Create(path + "link.txt");
            serverSocketActivation();
            sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                            ProtocolType.Udp);
            send_to_address = IPAddress.Parse(this.ip);
            sending_end_point = new IPEndPoint(send_to_address, PORT);
        }

        public void Send(Byte data)
        {
            sendByte(data);
        }

        async Task serverSocketActivation()
        {
            IPEndPoint e = new IPEndPoint(IPAddress.Any, PORT);
            UdpClient u = new UdpClient(e);
            UdpState s = new UdpState();
            s.e = end;
            s.u = u;

            Console.WriteLine("listening for messages");
            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

            // Do some work while we wait for a message. For this example, 
            // we'll just sleep 
            while (!messageReceived)
            {
                Thread.Sleep(100);
            }
        }

        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: {0}", receiveString);
            messageReceived = true;
        }

        async Task sendByte(Byte data)
        {
            Boolean done = false;
            Boolean exception_thrown = false;

            
            Console.WriteLine("Enter text to broadcast via UDP.");
            Console.WriteLine("Enter a blank line to exit the program.");
            while (!done)
            {
                byte[] send_buffer = new Byte[1];
                send_buffer[0]=data;

                try
                {
                    sending_socket.SendTo(send_buffer, sending_end_point);
                }
                catch (Exception send_exception )
                {
                    exception_thrown = true;
                    Console.WriteLine(" Exception {0}", send_exception.Message);
                }
                if (exception_thrown == false)
                {
                    done = true;
                    Console.WriteLine("Message has been sent to the broadcast address");
                }
                else
                {
                    exception_thrown = false;
                    Console.WriteLine("The exception indicates the message was not sent.");
                }
            }
        }

        public bool ping()
        {
            return true;
        }
    }
         * */
    }
}
