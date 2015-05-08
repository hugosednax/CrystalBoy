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
        const string remote_host = "127.0.0.1";
        const int remote_port = 10001;
        UdpClient client;
        System.Threading.Thread receiveThread;
        byte receivingByte;
        bool isReceiving = false;
        Object receiveByteLock;
        IPEndPoint ipE = null;

        public Link2()
        {
            UdpClient client = new UdpClient(remote_host, remote_port);
            receivingByte = new byte();
            receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
            connect();
        }

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        public void connect()
        {
            try
            {
                ipE = CreateIPEndPoint("other guy ip");
                client.Connect(ipE);
            }
            catch (Exception)
            {
                ipE = null;
            }
        }

        private void receive()
        {
            byte[] data = client.Receive(ref ipE);
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
            if (ipE == null)
                connect();
            if (ipE == null)
                return;
            client.SendAsync(data, data.Length);
        }
    }
}
