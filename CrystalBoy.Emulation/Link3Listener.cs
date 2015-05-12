using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CrystalBoy.Emulation
{
    class Link3Listener
    {
        private const int listenPort = 11000;
        UdpClient listener;
        IPEndPoint groupEP;
        System.Threading.Thread receiveThread;
        byte receivedData;
        Object dataLock = new Object();
        bool didReceive = false;

        public Link3Listener()
        {
            listener = new UdpClient(listenPort);
            groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            receiveThread = new System.Threading.Thread(new System.Threading.ThreadStart(Receive));
            receiveThread.Start();
            receivedData = new byte();
        }

        public void Receive(){
            while(true){
                byte[] data;
                try
                {
                    data = listener.Receive(ref groupEP);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    continue;
                }

                lock (dataLock)
                {
                    receivedData = data[data.Length - 1];
                }
                didReceive = true;
            }
        }

        public bool DidReceive()
        {
            return didReceive;
        }

        public byte GetReceived(){
            byte retData;
            lock(dataLock){
                retData = receivedData;
            }
            return receivedData;
        }

        public void SetDidReceive(bool didReceive)
        {
            this.didReceive = didReceive;
        }
    }
}
