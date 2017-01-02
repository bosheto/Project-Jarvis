using System;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace Jarvis
{
	public class Monitoring
	{
		//Debug mode On or Off
		private static bool debugMode = false;
		//SilentMode On or Off
		private static bool silentMode = false;
		//Program version
		string version;
		//Create a Speaech Synthesizer
		static SpeechSynthesizer synth = new SpeechSynthesizer();


		PerformanceCounter cpuPefr = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
		//Get Available Memory
		PerformanceCounter memPefr = new PerformanceCounter("Memory", "Available MBytes");


		#region
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
		static void Speak(string Message, int Rate, VoiceGender Gender)//Requires Message , Rate and Voice Gender         
		{
			synth.SelectVoiceByHints(Gender);
			Speak(Message, Rate);
		}
		#endregion

		public Monitoring(string _version)
		{
			
			//Get SilentMode value and convert it to float 
			int _SilentMode = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["SilentMode"]);

			if (_SilentMode == 1) { silentMode = true; }
			//Set version number 
			version = _version;
			//Print version number
			Console.WriteLine("Jarvis version {0}", version);
			//Speak version number
			Speak("System monitor version " + version + " enabled");
		}

		public void Monitor()
		{
			
			int CPULoad = (int)cpuPefr.NextValue();
			float RAMFree = memPefr.NextValue();

			//Print CPU Load
			Console.WriteLine("CPU load: {0} %", CPULoad);

			//Print available Ram 
			Console.WriteLine("Available memory: {0} MB", RAMFree);

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
		}
	}
}
