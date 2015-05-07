using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CrystalBoy.Emulation
{
    class Link2
    {
        const string remote_host = "127.0.0.1";
        const int remote_port = 10001;
        UdpClient client;
        System.Threading.Thread receiveThread;
        byte receivingByte;
        bool isReceiving = false;
        Object receiveByteLock;

        public Link2()
        {
            UdpClient client = new UdpClient(remote_host, remote_port);
            receivingByte = new byte();
            receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
        }

        private void receive()
        {
            IPEndPoint ep = null;
            byte[] data = client.Receive(ref ep);
            lock (receiveByteLock)
            {
                receivingByte = data[0];
            }
            isReceiving = true;
        }

        public byte getReceived()
        {
            lock (receiveByteLock)
            {
                return receivingByte;
            }
        }

        public bool didReceive()
        {
            return isReceiving;
        }

        public void send(byte[] data)
        {
            client.SendAsync(data, data.Length);
        }
    }
}
