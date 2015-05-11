using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace CrystalBoy.Emulation
{
    class Link2
    {
        const string remote_host = "25.102.132.160";
        const int remote_port = 10001;
        UdpClient client;
        System.Threading.Thread receiveThread;
        byte receivingByte;
        bool isReceiving = false;
        Object receiveByteLock;
        IPEndPoint ipE = null;
        bool isConnect = false;

        public Link2()
        {
            try
            {
                client = new UdpClient(remote_port);
                receivingByte = new byte();
                ipE = new IPEndPoint(IPAddress.Any, 10001);
                receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
                receiveThread.Start();
                System.Diagnostics.Debug.Write("Kappa");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.StackTrace);
            }
        }

        public void connect()
        {
            System.Diagnostics.Debug.Write("\r\n Tryng to connect");
            try
            {
                client.Connect("25.64.78.168", 10001);
                isConnect = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.StackTrace);
                isConnect = false;
            }
        }

        private void receive()
        {
            //System.Diagnostics.Debug.Write("Tryng to receive");
            while(true){
                if (isConnect != false && client != null)
                {
                    //System.Diagnostics.Debug.Write("HUUUUM");
                    byte[] data = client.Receive(ref ipE);
                    System.Diagnostics.Debug.Write("\r\n Received!");
                    lock (receiveByteLock)
                    {
                        receivingByte = data[0];
                    }
                    isReceiving = true;
                }
            }
        }

        public byte getReceived()
        {
            lock (receiveByteLock)
            {
                System.Diagnostics.Debug.Write("\r\n It's receiving bitcchh");
                return receivingByte;
            }
        }

        public bool didReceive()
        {
            return isReceiving;
        }

        public void send(byte[] data)
        {
            //System.Diagnostics.Debug.Write("Tryng to send");
            if (isConnect == false || client == null)
                connect();
            if (isConnect == false || client == null)
                return;
            //System.Diagnostics.Debug.Write("It's sending bitcchh");
            //client.SendAsync(data, data.Length);
            //System.Diagnostics.Debug.Write("\r\n Length: " + data.Length);
            client.Send(data, data.Length);
        }
    }
}
