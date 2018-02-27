using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightFX;

namespace AFX
{
    public interface ILight
    {
        string Name { get; }
        uint DeviceID { get; }
        LFX_Position Position { get; }
        LFX_ColorStruct LightColor { get; }
    }
}
