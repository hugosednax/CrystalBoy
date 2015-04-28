using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBoy.Emulation
{
    public class Link : MarshalByRefObject
    {
        GameBoyMemoryBus bus;

        public Link(GameBoyMemoryBus bus)
        {
            this.bus = bus;
        }
        public void send(Byte sendByte)
        {
            bus.WritePort(0x01, sendByte);
        }
        public void interrupt()
        {
            bus.RequestedInterrupts |= 0x08;
        }
    }
}
