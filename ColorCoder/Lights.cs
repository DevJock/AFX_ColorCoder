using System;
using System.Collections;
using System.Collections.Generic;

namespace AFX
{
    public class Lights : IEnumerable<ILight>
    {
        private static List<ILight> lights = new List<ILight>();

        public static int NumberOfDevices
        {
            get
            {
                return lights.Count;
            }
        }

        public static List<string> getDetailsFor(int listType)
        {
            List<string> list = new List<string>();
            foreach(ILight light in lights)
            {
                switch(listType)
                {
                    case 0:
                        {
                            list.Add(light.Name);
                        }break;
                    case 1:
                        {
                            list.Add(light.Position.ToString());
                        }
                        break;
                    case 2:
                        {
                            list.Add(light.LightColor.ToString());
                        }
                        break;
                }
            }
            return list;
        }


        public static ILight lightAtIndex(int i)
        {
            if(lights.Count > 0)
            {
                return lights[i];
            }
            return null;
        }


        public static int addDevice(ILight light)
        {
            if(!lights.Contains(light))
            {
                lights.Add(light);
                return 0;
            }
            return -1;
        }

        public static int removeDevice(ILight light)
        {
            if (lights.Contains(light))
            {
                lights.Remove(light);
                return 0;
            }
            return -1;
        }

        public static ILight getLightForName(string deviceName)
        {
            foreach(ILight light in lights)
            {
                if(light.Name == deviceName)
                {
                    return light;
                }
            }
            return null;
        }


        public IEnumerator<ILight> GetEnumerator()
        {
            foreach(var light in lights)
            {
                yield return light;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
