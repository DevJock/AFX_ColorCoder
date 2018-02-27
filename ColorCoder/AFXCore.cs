using System;
using LightFX;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFX
{
    class AFXCore
    {
        public static int UpdateInterval = 250;
        private static LightFXController lightFXController;
        private static uint AFX_Lights_Count;
        private static uint AFX_Device = 0;
        private static bool disable;



        public async void Update()
        {
            if(disable)
            {
                return;
            }
            List<ILight> lightsUsedNow = new List<ILight>();
            for(int i=0;i<UpdateLights.NumberOfDevices;i++)
            {
                LightDevice device = UpdateLights.lightAtIndex(i) as LightDevice;
                if(lightsUsedNow.Contains(device))
                {
                    UpdateLights.pushToStack(device);
                    continue;
                }
                Colorize(AFX_Device, device.DeviceID, device.LightColor);
                UpdateLights.removeDevice(device);
            }
            UpdateLights.resetStack();
            await Task.Delay(UpdateInterval);
            Update();
        }


        private static int Colorize(uint DeviceID, uint LightID, LFX_ColorStruct Color)
        {
            if (disable)
            {
                return -1;
            }
            LFX_Result result = lightFXController.LFX_SetLightColor(DeviceID, LightID, Color);
            lightFXController.LFX_Update();
            return result == LFX_Result.LFX_Success ? 0 : -1;
        }




        public List<string> getNames()
        {
            if (disable)
            {
                return null;
            }
            return Lights.getDetailsFor(0);
        }

        public List<string> getPositions()
        {
            if (disable)
            {
                return null;
            }
            return Lights.getDetailsFor(1);
        }

        public List<string> getColors()
        {
            if (disable)
            {
                return null;
            }
            return Lights.getDetailsFor(2);
        }

        public void InitializeAFX()
        {
            lightFXController = new LightFXController();
            LFX_Result status = lightFXController.LFX_Initialize();
            Console.WriteLine("Initializing Alien FX System");
            switch (status)
            {
                case LFX_Result.LFX_Success:
                    {
                        Console.WriteLine("Initialization Successful");
                        lightFXController.LFX_Reset();
                        lightFXController.LFX_GetNumLights(AFX_Device, out AFX_Lights_Count);
                        Lights lights = new Lights();
                        for(int i=0;i<AFX_Lights_Count;i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            lightFXController.LFX_GetLightDescription(AFX_Device, Convert.ToUInt32(i), out sb, 255);
                            string name = sb.ToString();
                            LFX_Position position;
                            lightFXController.LFX_GetLightLocation(AFX_Device, Convert.ToUInt32(i), out position);
                            LFX_ColorStruct color;
                            lightFXController.LFX_GetLightColor(AFX_Device, Convert.ToUInt32(i), out color);
                            LightDevice device = new LightDevice(Convert.ToUInt32(i),name, position, color);
                            Lights.addDevice(device);
                        }
                    }
                    break;
                case LFX_Result.LFX_Failure:
                    {
                        disable = true;
                        Console.WriteLine("Error Occured During Initialization.");
                    }
                    break;
                default:
                    {
                        disable = true;
                        Console.WriteLine("Not An Alien FX Equipped Machine");
                    }
                    break;
            }
        }

        public int ReleaseAFX()
        {
            lightFXController.LFX_Release();
            return 0;
        }

    }
}
