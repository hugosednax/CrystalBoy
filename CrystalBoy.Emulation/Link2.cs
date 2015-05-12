using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Diagnostics;
using System.Threading;

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
        bool isSending = false;
        Object receiveByteLock = new Object();
        IPEndPoint ipE = null;
        bool isConnect = false;
        Stopwatch stopWatch = new Stopwatch();

        public Link2()
        {
            stopWatch.Start();
            try
            {
                client = new UdpClient(remote_port);
                client.Client.SendTimeout = 100;
                client.Client.ReceiveTimeout = 100;
                receivingByte = new byte();
                ipE = new IPEndPoint(IPAddress.Any, 10001);
                receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(receive));
                receiveThread.Start();
                //System.Diagnostics.Debug.Write("Kappa");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.StackTrace);
            }
        }

        public void connect()
        {
            //.Diagnostics.Debug.Write("\r\n Tryng to connect");
            try
            {
                client.Connect("25.102.132.160", 10001);
                isConnect = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                //System.Diagnostics.Debug.Write(ex.StackTrace);
                isConnect = false;
            }
        }

        private void receive()
        {
            //System.Diagnostics.Debug.Write("Tryng to receive");
            /*Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            stopWatch.Stop();*/
            while(true){
                //System.Diagnostics.Debug.WriteLine("Going to sleep");
                //Thread.Sleep(1000);
                //System.Diagnostics.Debug.WriteLine("Awake");
                if (isConnect != false && client != null)
                {
                    //System.Diagnostics.Debug.WriteLine("Checking for received msgs");
                    //System.Diagnostics.Debug.Write("HUUUUM");
                    byte[] data;
                    try
                    {
                        data = client.Receive(ref ipE);
                    }
                    catch (SocketException e)
                    {
                        //isConnect = false;
                        System.Diagnostics.Debug.WriteLine(e.StackTrace);
                        continue;
                    }

                    System.Diagnostics.Debug.WriteLine("Received " + data[0].ToString());
                    //System.Diagnostics.Debug.Write("\r\n Received!");
                    lock (receiveByteLock)
                    {
                        receivingByte = data[data.Length-1];
                    }
                    isReceiving = true;
                }
            }
        }

        public byte getReceived()
        {
            byte retByte;
            lock (receiveByteLock)
            {
                //System.Diagnostics.Debug.Write("\r\n It's receiving bitcchh");
                retByte = receivingByte;
            }
            return retByte;
        }

        public void setReceived(bool received)
        {
            isReceiving = false;
        }

        public bool didReceive()
        {
            return isReceiving;
        }

        public bool didSend()
        {
            return isSending;
        }

        public void setSending(bool isSending)
        {
            this.isSending = isSending;
        }

        public void send(byte[] data)
        {
            receivingByte = data[0];
            isReceiving = true;
            /*if (stopWatch.ElapsedMilliseconds > 100)
            {
                stopWatch.Restart();*/
                //System.Diagnostics.Debug.WriteLine("Tryng to send");
                if (isConnect == false || client == null)
                {
                    try{
                        connect();
                        client.SendAsync(data, data.Length);
                        isSending = true;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.StackTrace);
                        isSending = false;
                    }
                }
            //}
        }
    }
}
