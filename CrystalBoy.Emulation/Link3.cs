using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CrystalBoy.Emulation
{
    class Link3
    {
        Socket sending_socket;
        IPAddress send_to_address;
        IPEndPoint sending_end_point;

        public Link3()
        {
            sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            send_to_address = IPAddress.Parse("25.102.132.160");
            sending_end_point = new IPEndPoint(send_to_address, 11000);
        }

        public void Send(byte dataByte){
            byte[] send_buffer = new byte[1];
            send_buffer[0] = dataByte;
            bool exception_thrown = false;

            try{
                sending_socket.SendTo(send_buffer, sending_end_point);
            }catch (Exception send_exception ){
                exception_thrown = true;
                System.Diagnostics.Debug.WriteLine(" Exception {0}", send_exception.Message);
            }
            if (exception_thrown == false){
                Console.WriteLine("Message has been sent to the broadcast address");
            }else{
                exception_thrown = false;
                System.Diagnostics.Debug.WriteLine("The exception indicates the message was not sent.");
            }
        }
    }
}
