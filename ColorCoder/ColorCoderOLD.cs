using System;
using LightFX;
using System.Text;
using System.Collections.Generic;


namespace AFX
{
    public class ColorCoderOLD
    {
        public static string[] AFX_Keyboard = { "Keyboard Left", "Keyboard Middle Left", "Keyboard Middle Right", "Keybo ard Right" };
        public static string[] AFX_Tron = { "LCD Panel Left", "LCD Panel Right", "Left Tron", "Right Tron" };
        public static string AFX_TouchPad = "Touchpad";
        public static string AFX_Logo = "Logo";
        public static string AFX_AlienHead = "Alien Head";
        private static uint AFX_Device = 0;
        private static uint AFX_Lights_Count;
        private Random random = new Random();
        private LightFXController lfxController;
        private  List<string> lightDescriptions = new List<string>();
        private List<uint> lights = new List<uint>();


        private uint idForDeviceName(string device)
        {
            for(int i=0;i<lightDescriptions.Count;i++)
            {
                if(String.Equals(device,lightDescriptions[i]))
                {
                    return lights[i];
                }
            }
            return Convert.ToUInt32(-1);
        }






        public LFX_ColorStruct newRandomColor()
        {
            byte r = Convert.ToByte(random.Next(0, 255));
            byte g = Convert.ToByte(random.Next(0, 255));
            byte b = Convert.ToByte(random.Next(0, 255));
            return new LFX_ColorStruct(255, r, g, b);
        }
        public uint newRandomLight()
        {
            return Convert.ToUInt32(random.Next(0, Convert.ToInt32(AFX_Lights_Count)));
        }

        public int setZoneToColor(string deviceName, LFX_ColorStruct color)
        {

            uint light = idForDeviceName(deviceName);

            int result = lfxController.LFX_SetLightColor(AFX_Device, light, color) == LFX_Result.LFX_Success ? 0 : 1;
            lfxController.LFX_Update();
            return result;
        }

        public int resetZone(string deviceName)
        {
            LFX_ColorStruct color = new LFX_ColorStruct(0, 0, 0, 0);
            uint light = idForDeviceName(deviceName);
            int result = lfxController.LFX_SetLightColor(AFX_Device, light, color) == LFX_Result.LFX_Success ? 0 : 1;
            lfxController.LFX_Update();
            return result;
        }


        public int setZoneToColor(string deviceName, bool randomColor)
        {
            LFX_ColorStruct color;
            if(!randomColor)
            {
                color = new LFX_ColorStruct(255, 255, 0, 0);
            }
            else
            {
                color = newRandomColor();
            }
            uint light = idForDeviceName(deviceName);
            int result = lfxController.LFX_SetLightColor(AFX_Device, light, color) == LFX_Result.LFX_Success ? 0:1;
            lfxController.LFX_Update();
            return result;
        }

        private string nameForDeviceWithID(uint id)
        {
            for(int i=0;i<lights.Count;i++)
            {
                if(lights[i] == id)
                {
                    return lightDescriptions[i];
                }
            }
            return "NULL";
        }

        public int setRandomColorToRandomZone()
        {
            return setZoneToColor(nameForDeviceWithID(Convert.ToUInt32(random.Next(0, lights.Count))), true);
        }

        public void Initialize()
        {
            lfxController = new LightFXController();
            LFX_Result status = lfxController.LFX_Initialize();
            Console.WriteLine("Initializing Alien FX System");
            switch (status)
            {
                case LFX_Result.LFX_Success:
                    {
                        Console.WriteLine("Initialization Successful");
                        lfxController.LFX_Reset();
                        lfxController.LFX_GetNumLights(AFX_Device, out AFX_Lights_Count);
                        for(int i=0;i<AFX_Lights_Count;i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            lfxController.LFX_GetLightDescription(AFX_Device, Convert.ToUInt32(i),out sb, 255);
                            lightDescriptions.Add(sb.ToString());
                            lights.Add(Convert.ToUInt32(i));
                        }
                        Console.WriteLine("Done Initializing.");
                    }
                    break;
                case LFX_Result.LFX_Failure:
                    {
                        Console.WriteLine("Error Occured During Initialization.");
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Not An Alien FX Equipped Machine");
                    }
                    break;
            }
        }

        public int ReleaseAFX()
        {
            lfxController.LFX_Reset();
            lfxController.LFX_Release();
            return 0;
        }
    }
}