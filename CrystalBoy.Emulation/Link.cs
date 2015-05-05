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
        GameBoyMemoryBus bus;
        string path = @"..\output\";
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

        public void Send(Byte sendByte)
        {
            bus.activateLink(sendByte);
            try
            {
                byteDataSend[0] = sendByte;

                //Send it to the server
                clientSocket.BeginSend(byteDataSend, 0, byteDataSend.Length,
                    SocketFlags.None,
                    new AsyncCallback(OnSend), null);
            }
            catch (Exception)
            {
                //TODO message
            }  
        }

        private void serverSocketActivation()
        {
            try
            {
                //We are using TCP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);

                //Assign the any IP of the machine and listen on port number 1000
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 10001);

                //Bind and listen on the given address
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);

                //Accept the incoming clients
                serverSocket.BeginReceive(byteDataReceive, 0, byteDataReceive.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception ex)
            {
                //TODO exception message
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                byte received = byteDataReceive[0];
                bus.WritePort(0x01, received);
                //TODO write received byte to bus

                serverSocket.BeginReceive(byteDataReceive, 0, byteDataReceive.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception ex)
            {
                //TODO error message
            }
        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                //TODO error message
            }
        }

        public bool ping()
        {
            return true;
        }
    }
}
