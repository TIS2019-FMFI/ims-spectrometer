using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Management;
using Arduin.Backend.Model;

namespace Arduin.Backend{

    // SINGLETON , use as ArduinoConnectionService.instance.XXXX !!!
    class ArduinoConnectionService {
        // SINGLETON , use as ArduinoConnectionService.instance.XXXX !!!
        private static ArduinoConnectionService instance;

        private ArduinoConnectionService() { }

        public static ArduinoConnectionService Instance{
            get{
                if (instance == null){
                    instance = new ArduinoConnectionService();
                }
                return instance;
            }
        }


        private SerialPort serial = new SerialPort();


        /**
         * open serial connection to arduino on available COM port
         */
        public bool start(){
            // TODO !!!! 
            return false;

        }

        /**
         * reading data from arduino, one measurement cycle will last maximum 20 000 microseconds
         */ 
        public Measurement getMeasurementFromArduino(){
            Measurement oneMeasurementCycle = new Measurement();
            // TODO !!!! 


            return oneMeasurementCycle;

        }

        internal void sendCommand(string text){
            if (serial.IsOpen)
                serial.Write(text);
        }

        private static string portFromName(string name = "Infineon DAS JDS COM"){
            //find the port accrding to name
            var port = "";
            using (var mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
                foreach (ManagementObject ManObj in mos.Get())
                {
                    var s = ManObj["Name"].ToString();
                    if (s.Contains(name)) 
                        port = s.Substring(s.LastIndexOf('(') + 1, s.LastIndexOf(')') - s.LastIndexOf('(') - 1);
                }
            //id still no port use the last COMxx
            if (port == "") {
                var portNames = SerialPort.GetPortNames();
                if (portNames.Length > 0) 
                    port = portNames[portNames.Length - 1];
            }
            return port;
        }
    }
}
