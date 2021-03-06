using System;
using System.Threading;
namespace Jarvis
{
    class ProgramMain
    {

        //Version
        static string version = "1.0.3";
        //Check set debuging mode ON or OFF
        static bool Debuging = false;

		// Entry Point

		static void Main(string[] args)
        {
            //Create a Monitoring Object
			Monitoring monitoring = new Monitoring(version, Debuging );
            
            //Main Program Loop(infinite)
            while (true)
            {   //
				monitoring.Monitor();
                //Print line to make reading easy
                Console.WriteLine("--------------------------------");
                //Time in ms to refresh
                Thread.Sleep(1000);
            }//END OF LOOP
		}
    }
}
