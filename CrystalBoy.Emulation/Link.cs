using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrystalBoy.Emulation
{
    public class Link : MarshalByRefObject
    {
        GameBoyMemoryBus bus;
        string path = @"C:\Users\hugo__000\Desktop\";
        FileStream fileOpcodeStream;

        public Link(GameBoyMemoryBus bus)
        {
            this.bus = bus;
            fileOpcodeStream = File.Create(path + "link.txt");
        }
        public void Send(Byte sendByte)
        {
            bus.WritePort(0x01, sendByte);
            bus.RequestedInterrupts |= 0x08;
            byte newValue = bus.ReadPort(0x02);
            newValue |= 0x80;
            bus.WritePort(0x02, newValue);
            string portStrB = "Remote call! \r\n";
            byte[] bytesInStreamB = GameBoyMemoryBus.GetBytes(portStrB);
            //fileOpcodeStream.Read(bytesInStream, 0, bytesInStream.Length);
            fileOpcodeStream.Write(bytesInStreamB, 0, bytesInStreamB.Length);
        }
    }
}
