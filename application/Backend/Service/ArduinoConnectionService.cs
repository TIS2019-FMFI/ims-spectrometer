﻿using System;
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


        private static SerialPort serial = new SerialPort();


        /**
         * open serial connection to arduino on available COM port
         */
        public bool start(){
            if (serial.IsOpen) {
                return serial.IsOpen;
            }

            try {
                serial.BaudRate = 2000000;
                serial.PortName = portFromName();
                serial.Open();
            } catch (Exception ex) {
                throw new ApplicationException("Error opening my port: " + ex.Message);
            } 

            Task.Delay(100).Wait(); //allow Arduino reset 
            
            return serial.IsOpen;

        }

        /**
         * reading data from arduino, one measurement cycle will last maximum 20 000 microseconds
         */
        public Measurement getMeasurementFromArduino() {
            
            string line = "";
            int position = 0;
            int[] buffer = new int[Measurement.BUFFER_SIZE];

            while (serial.IsOpen) {
                try {
                    line = serial.ReadLine().Trim(); // delete whitespace

                    if (line.Equals("START")) {
                        buffer = new int[Measurement.BUFFER_SIZE];
                        position = 0;
                    } else if (line.Equals("END")) {
                        Measurement oneMeasurementCycle = new Measurement();
                        oneMeasurementCycle.measurement = new int[position];
                        Array.Copy(buffer, oneMeasurementCycle.measurement, position);

                        return oneMeasurementCycle;
                    } else {
                        try {
                            buffer[position++] = Convert.ToInt16(line);
                        } catch (Exception ex) {
                            position = 0;
                            Console.WriteLine("Error occurred converting : " + line + " from serial port do double : " + ex.Message);
                        }
                    }
                } catch { }


            }
            Console.WriteLine("No serial connection, could not read data");
            throw new Exception("No connection");
        }


        /**
         * will send gate and sampling to arduino in format "gate sampling"
         */
        public void sendSettingsToArduino(){
            try {
                if (serial.IsOpen) {
                    // HW requirements gate accumulating by 2, sampling by 5
                    String send = (Settings.gate / 2).ToString() + (Settings.sampling / 5).ToString();
                    Console.WriteLine(send);
                    serial.Write(send);

                    // removed data inside buffer
                    serial.DiscardOutBuffer();
                    serial.DiscardInBuffer();

                    return;
                }
                throw new ApplicationException("No connection , can not send settings to Arduino");
            } catch (Exception error) {
                throw new ApplicationException("error " + error.Message);
            }
        }

        private string portFromName(string name = "Infineon DAS JDS COM"){
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
