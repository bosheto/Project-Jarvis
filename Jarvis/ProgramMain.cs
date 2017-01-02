using System;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;
namespace Jarvis
{
    class ProgramMain
    {

        //Version
        static string version = "1.0.2";
		 //Debug mode On or Off
       

        // Entry Point
        static void Main(string[] args)

        {
			Monitoring monitoring = new Monitoring(version);
            //Main Program Loop(infinite)
            while (true)
            {
				monitoring.Monitor();
         	    Console.WriteLine("--------------------------------");
                Thread.Sleep(1000);

            }//END OF LOOP
		}
    }
}
