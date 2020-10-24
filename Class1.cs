//
//      ===  LCDSmartie Plugin for openhardwaremonitor  ===
//              Author: erland@hedmanshome.se
//
// dot net plugins are supported in LCD Smartie 5.3 beta 3 and above.
// Data Interface

// The Open Hardware Monitor publishes all sensor data to WMI (Windows Management Instrumentation).
// This allows other applications to read and use the sensor information as well.

// You may provide/use upto 20 functions (function1 to function20).

using System;
using System.Management;
//using System.Windows.Forms;

namespace fanspeed
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class LCDSmartie
	{
		public LCDSmartie()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		// This function is used in LCDSmartie by using the dll command as follows:
		//    $dll(fanspeed,1,CPU Package,0)
		public string function1(string param1, string param2)
		{
			return GetCPUTemperature(param1).ToString();
		}

		// This function is used in LCDSmartie by using the dll command as follows:
		//	  $dll(fanspeed,2,CPU,0)
		public string function2(string param1, string param2)
		{
			return  Math.Round(GetCPUfanSpeed(param1)).ToString();
		}

		//
		// Define the minimum interval that a screen should get fresh data from our plugin.
		// The actual value used by Smartie will be the higher of this value and of the "dll check interval" setting
		// on the Misc tab.  [This function is optional, Smartie will assume 300ms if it is not provided.]
		// 
		public int GetMinRefreshInterval()
		{
			return 300; // 300 ms (around 3 times a second)
		}
		private float  GetCPUTemperature(string source)
        {
			float CpuPackageTemp = 0;
			try
			{
				ManagementObjectSearcher searcher =
				  new ManagementObjectSearcher("ROOT\\OpenHardwareMonitor",
				  "SELECT * FROM Sensor WHERE Name ='"+source+"' AND SensorType = 'Temperature'");

				foreach (ManagementObject queryObj in searcher.Get())
				{
					CpuPackageTemp = (float)queryObj["Value"];
					queryObj.Dispose();
				}

				searcher.Dispose();

			}
			catch (ManagementException)
			{
				CpuPackageTemp = -1;
			}
			return CpuPackageTemp;
		}

		private float GetCPUfanSpeed(string source)
		{
			float fanSpeed = 0;
			try
			{
				ManagementObjectSearcher searcher =
				  new ManagementObjectSearcher("ROOT\\OpenHardwareMonitor",
				  "SELECT * FROM Sensor WHERE Name ='"+source+"' AND SensorType = 'Fan'");

				foreach (ManagementObject queryObj in searcher.Get())
				{
					fanSpeed = (float)queryObj["Value"];
					//MessageBox.Show("Value=" + fanSpeed, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					queryObj.Dispose();
				}

				searcher.Dispose();

			}
			catch (ManagementException)
			{
				fanSpeed = 0;
			}
			return fanSpeed;
		}

	}

}
