using System;
using System.Collections.Generic;
using LightFX;

namespace AFX
{
    public class UpdateLights 
    {
        private static List<ILight> lights = new List<ILight>();
        private static List<ILight> nextLights = new List<ILight>();
        public static int NumberOfDevices
        {
            get
            {
                return lights.Count;
            }
        }

        public static int StackSize
        {
            get
            {
                return nextLights.Count;
            }
        }

        public static void resetStack()
        {
            foreach(var light in nextLights)
            {
                lights.Add(light);
                nextLights.Remove(light);
            }
        }

        public static void pushToStack(ILight light)
        {
            nextLights.Add(light);
        }


        public static ILight lightAtIndex(int i)
        {
            if (lights.Count > 0)
            {
                return lights[i];
            }
            return null;
        }

        public static int addDevice(string lightName, string ColorCSV)
        {
            string[] components = ColorCSV.Split(',');
            LFX_ColorStruct color = new LFX_ColorStruct(Convert.ToByte(components[0]), Convert.ToByte(components[1]), Convert.ToByte(components[2]), Convert.ToByte(components[3]));
            LightDevice device = Lights.getLightForName(lightName) as LightDevice;
            device.LightColor = color;
            lights.Add(device);
            return 0;
        }

        public static int removeDevice(ILight light)
        {
            if (lights.Contains(light))
            {
                try
                {
                    lights.Remove(light);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error Occured: {0}",e.ToString());
                }
                return 0;
            }
            return -1;
        }
    }
}
