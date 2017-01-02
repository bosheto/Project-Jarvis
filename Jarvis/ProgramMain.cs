using System;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;
namespace Jarvis
{
    class ProgramMain
    {

        //Version
        static string version = "1.0.1";
        //Window Height and Widght
        private static int _height = 10;
        private static int _widght = 40;
        //Debug mode On or Off
        private static bool debugMode = true;
        //Silent Mode On or Off
        private static bool silentMode = false;
        //Create a Speaech Synthesizer
        static SpeechSynthesizer synth = new SpeechSynthesizer();
       

        // Entry Point
        static void Main(string[] args)

        {
            //Setup window size 
            Console.SetWindowSize(_widght , _height);

            //Print out Program version
            Console.WriteLine("Jarvis version {0}", version);

            //Send Voice message to User
            Speak("System monitor version"+ version +"enabled"  , 1 , VoiceGender.Female);
            
            //Perfomance Counters
            #region Perfomance Counters 

            //Get CPU load
            PerformanceCounter cpuPefr = new PerformanceCounter("Processor Information" , "% Processor Time" , "_Total");
            //Get Available Memory
            PerformanceCounter memPefr = new PerformanceCounter("Memory", "Available MBytes");

            #endregion
          
            //Main Program Loop(infinite)
            while (true)
            {
                int CPULoad = (int)cpuPefr.NextValue();
                float RAMFree = memPefr.NextValue();

                //Print CPU Load
                Console.WriteLine("CPU load: {0} %", CPULoad);

                //Print available Ram 
                Console.WriteLine("Available memory: {0} MB", RAMFree );
              
                
            
                //Curent values Speach
                if (CPULoad > 80)
                {
                    if (CPULoad == 100)
                    {
                       
                        string CPUVocalMessage = String.Format("Warnig maximum cpu load");
                        Speak(CPUVocalMessage);
                    }
                    else
                    {
                       string CPUVocalMessage = String.Format("Warnig high CPU Load");
                       Speak(CPUVocalMessage);
                    }
                }
                Console.WriteLine("-------------------------------");
                Thread.Sleep(1000);
            }//END OF LOOP

        
        }

        //Static Speak Function 
        #region Speak()
        static void Speak(string Message)//Only requires a Messege
        {
            //Check if Silent mode is on or off 
            if (silentMode == false)
            {
                //Check if debug mode is on or off 
                if (debugMode == false)
                {
                    synth.Speak(Message);
                }
            }
        }
        static void Speak(string Message, int Rate)//Requires Message and Rate
        {
            synth.Rate = Rate;
            Speak(Message);
        }
        static void Speak(string Message , int Rate , VoiceGender Gender)//Requires Message , Rate and Voice Gender         
        {
            synth.SelectVoiceByHints(Gender);
            Speak(Message, Rate);         
        }
        #endregion
    }
}
