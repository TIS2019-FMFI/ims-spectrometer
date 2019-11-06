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
    public class ArduinoConnectionService {
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
            serial.BaudRate = 2000000;
            serial.PortName = portFromName();
            
            try {
                serial.Open();
            } catch (Exception ex) {
                Console.WriteLine("Error opening my port: {0}", ex.Message);
            }

            Task.Delay(100).Wait(); //allow Arduino reset 
            
            return serial.IsOpen;

        }

        /**
         * reading data from arduino, one measurement cycle will last maximum 20 000 microseconds
         */
        public Measurement getMeasurementFromArduino() {
            Measurement oneMeasurementCycle = new Measurement();
            string line = "";
            int position = 0;

            while (serial.IsOpen) {
                line = serial.ReadLine();
                Console.WriteLine(line); // later DELETE 

                if (line.Equals("START")) {
                    oneMeasurementCycle = new Measurement();
                    position = 0;
                } else if (line.Equals("END")) {
                    return oneMeasurementCycle;
                } else {
                    try {
                        oneMeasurementCycle.measurement[position++] = Convert.ToDouble(line);
                        oneMeasurementCycle.validData++;
                    } catch (Exception ex) {
                        Console.WriteLine("Error occured converting data from serial port do double : " + ex.Message);
                    }
                }

            }
            Console.WriteLine("No serial connection, could not read data");
            return null;

        }


        /**
         * will send gate and sampling to arduino in format "gate sampling"
         */
        internal void sendSettingsToArduino(){
            if (serial.IsOpen)
                serial.Write(Settings.gate + " " + Settings.sampling); // + Environment.NewLine
        }

        private static string portFromName(string name = "Infineon DAS JDS COM"){
            //find the port accrding to name
            var port = "";
            using (var mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
                foreach (ManagementObject ManObj in mos.Get()){
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
