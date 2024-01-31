using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CNCMonitor
{
    public class CNCMachineDataAnalyzer
    {
        private double temperatureThreshold = 35.0;
        private double dimensionThreshold = 0.05;
        private string statusCode = "0xFF";
        CNCMonitorData CNCMonitorObj;
        public event Action actionEvent;
        public DateTime OpenedTime;
        TimerManager timerObj;
        public CNCMachineDataAnalyzer(CNCMonitorData data) {
            this.CNCMonitorObj = data;
            
        }
        public void AnalyzeTemperature()
        {
            if (CNCMonitorObj.temperature > temperatureThreshold)
            {
                IAlertManager tempAlertObj = new TemperatureAlertManager();
                Action tempAction = new Action(tempAlertObj.GenerateAlert);
                this.actionEvent += tempAction;
                
            }
        }
        public void AnalyzeDuration()
        {
            OpenedTime = DateTime.Now;
            Thread.Sleep(3000);
            timerObj = new TimerManager(this);
            
            if (timerObj.CheckDuration())
            {
                IAlertManager durationAlertObj = new DurationAlertManager();
                Action durationAction = new Action(durationAlertObj.GenerateAlert);
                this.actionEvent += durationAction;
                
            }

        }
        public void AnalyzeDimension()
        {
            
            if (CNCMonitorObj.dimension > dimensionThreshold)
            {
                IAlertManager dimensionAlertObj = new DimensionAlertManager();
                Action dimensionAction = new Action(dimensionAlertObj.GenerateAlert);
                this.actionEvent += dimensionAction;
               
                
            }
            
        }
        public void AnalyzeStatusCode()
        {
            if (CNCMonitorObj.code != statusCode)
            {
                IAlertManager statusCodeAlertObj = new StatusCodeAlertManager();

                Action statusAction = new Action(statusCodeAlertObj.GenerateAlert);
                this.actionEvent += statusAction;
            }
        }
        public void ExecuteActions()
        {
            if (actionEvent != null)
            {
                actionEvent.Invoke();
            }
        }
    }
}
