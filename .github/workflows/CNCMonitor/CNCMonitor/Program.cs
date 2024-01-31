using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCMonitor
{
    public class Program
    {
        static void Main(string[] args)
        {

            CNCMonitorData data = new CNCMonitorData(40.5, 0.03, 1, "0xFF");
            CNCMachineDataAnalyzer cNCMachineDataAnalyzer = new CNCMachineDataAnalyzer(data);

            

            cNCMachineDataAnalyzer.AnalyzeTemperature();
            cNCMachineDataAnalyzer.AnalyzeDuration();
            cNCMachineDataAnalyzer.AnalyzeDimension();
            cNCMachineDataAnalyzer.AnalyzeStatusCode();
            cNCMachineDataAnalyzer.ExecuteActions();
            Console.ReadLine();
        }
    }
}
