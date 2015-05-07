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
        private static const int PORT = 10001;

        GameBoyMemoryBus bus;
        string path = @"..\..\..\output\";
        FileStream fileOpcodeStream;
        string ip = "120.120.120.120"; // hardcoded IP
        Socket serverSocket, clientSocket;
        byte[] byteDataReceive = new byte[1];
        byte[] byteDataSend = new byte[1];


        public Link(GameBoyMemoryBus bus)
        {
            this.bus = bus;
            fileOpcodeStream = File.Create(path + "link.txt");
            serverSocketActivation();
        }
        public Link(GameBoyMemoryBus bus, string ip)
        {
            this.bus = bus;
            this.ip = ip;
            fileOpcodeStream = File.Create(path + "link.txt");
            serverSocketActivation();
        }

        public void Send(Byte data)
        {
            sendByte(data);
        }

        async Task serverSocketActivation()
        {
            bool done = false;
            UdpClient listener = new UdpClient(PORT);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, PORT);
            string received_data;
            byte[] receive_byte_array;
            try
            {
                while (!done)
                {
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = receive_byte_array[0].ToString();
                    Console.WriteLine("data follows \n{0}\n\n", received_data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            listener.Close();            
        }

        async Task sendByte(Byte data)
        {
            Boolean done = false;
            Boolean exception_thrown = false;

            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                            ProtocolType.Udp);
            IPAddress send_to_address = IPAddress.Parse(this.ip);
            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, PORT);
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
}
