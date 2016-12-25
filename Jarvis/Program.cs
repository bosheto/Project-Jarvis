using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;
namespace Jarvis
{
    class Program
    {

        //Version
        string version = "1.0";
        //Window Height and Widght
        private static int _height = 10;
        private static int _widght = 40;
        //Create a Speaech Synthesizer
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        

        //User Choise bools
        static bool MonitorCpu = true;
        static bool MonitorRam = true;


        // Entry Point
        static void Main(string[] args)

        {
            
            
            //Set up Window size
            Console.SetWindowSize(_widght,_height);
            

            //Send Voice message to User
            Speak("System monitor enabled" , 1 , VoiceGender.Female);

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

                //Check if user has set cpu monitoring to true 
                if(MonitorCpu == true)
                {   
                    //Print CPU Load
                    Console.WriteLine("CPU Load: {0}", CPULoad + " %");
                }
                //Check if user has set ram monitoring to true 
                if(MonitorRam == true)
                {
                    //Print available Ram 
                    Console.WriteLine("Available Memory: {0} MB", RAMFree );
                }
                Console.WriteLine("--------------------------------");
                
            
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
               
                Thread.Sleep(1000);
            }//END OF LOOP

        
        }

        //Static Speak Function 
        #region Speak()
        static void Speak(string Message)//Only requires a Messege
        {
            synth.Speak(Message);
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
