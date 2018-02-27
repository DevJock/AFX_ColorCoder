using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFX
{
    public class AFXControl
    {
        public static List<string> Names = new List<string>();
        public static List<string> Positions = new List<string>();
        public static List<string> Colors = new List<string>();
        static Random r = new Random();
        static AFXCore afx = new AFXCore();
        static bool demo;
        static int randomValues()
        {
            return r.Next(51, 255);
        }


        public static void Initialize()
        { 
            afx.InitializeAFX();
            Names = afx.getNames();
            Positions = afx.getPositions();
            Colors = afx.getColors();
            afx.Update();
        }

        public static void Release()
        {
            afx.ReleaseAFX();
        }

        public static void setColorToLight(string Name,string ColorCSV)
        {
            UpdateLights.addDevice(Name, ColorCSV);
        }

        public static async void run()
        {
            demo = true;
            while (demo)
            {
                UpdateLights.addDevice(Names[r.Next(0, Names.Count)], $"{randomValues()},{randomValues()},{randomValues()},{randomValues()},");

                Console.WriteLine($"{UpdateLights.NumberOfDevices}, {UpdateLights.StackSize}");
                await Task.Delay(AFXCore.UpdateInterval);
            }
        }

        public static void stop()
        {
            demo = false;
        }

    }
}
