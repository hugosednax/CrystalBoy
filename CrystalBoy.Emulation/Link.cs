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
        public async void Send(Byte sendByte)
        {
            bus.activateLink(sendByte);
        }

        public bool ping()
        {
            return true;
        }
    }
}
