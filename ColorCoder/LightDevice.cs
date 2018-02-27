using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightFX;

namespace AFX
{
    public class LightDevice : ILight
    {
        private string _name;
        private uint _deviceID;
        private LFX_Position _position;
        private LFX_ColorStruct _lightColor;


        public LightDevice(uint deviceID,string name,LFX_Position position,LFX_ColorStruct lightColor)
        {
            _deviceID = deviceID;
            _name = name;
            _position = position;
            _lightColor = lightColor;
            Console.WriteLine($"{Name} Attached with Lights Set to {LightColor} color Lights.");
        }


        public string Name
        {
            get
            {
                return _name;
            }
        }

        public uint DeviceID
        {
            get
            {
                return _deviceID;
            }
        }

        public LFX_Position Position
        {
            get
            {
                return _position;
            }
        }

        public LFX_ColorStruct LightColor
        {
            get
            {
                return _lightColor;
            }
            set
            {
                _lightColor = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} {Position.ToString()} {LightColor.ToString()}";
        }
    }
}
