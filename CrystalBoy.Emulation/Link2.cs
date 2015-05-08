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
                client = new UdpClient(remote_host, remote_port);
                receivingByte = new byte();
                ipE = new IPEndPoint(IPAddress.Any, 10001);
                receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
                receiveThread.Start();
                connect();
                System.Diagnostics.Debug.Write("Kappa");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.StackTrace);
            }
        }

        public void connect()
        {
            System.Diagnostics.Debug.Write("Tryng to connect");
            try
            {
                client.Connect("25.71.55.98", 10001);
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
            System.Diagnostics.Debug.Write("Tryng to receive");
            if (isConnect != null && client != null)
            {
                byte[] data = client.Receive(ref ipE);
                lock (receiveByteLock)
                {
                    receivingByte = data[0];
                }
                isReceiving = true;
            }
            else
                receive();
        }

        public byte getReceived()
        {
            lock (receiveByteLock)
            {
                System.Diagnostics.Debug.Write("It's receiving bitcchh");
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
            if (isConnect == null || client == null)
                connect();
            if (isConnect == null || client == null)
                return;
            //System.Diagnostics.Debug.Write("It's sending bitcchh");
            client.SendAsync(data, data.Length);
        }
    }
}
