using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduin.Backend.Model;
using System.Threading;

namespace Arduin.Backend{
    // SINGLETON , use as DataManagementService.instance.XXXX !!!
    class DataManagementService {
        // SINGLETON , use as DataManagementService.instance.XXXX !!!
        private static DataManagementService instance;

        private DataManagementService() { }

        public static DataManagementService Instance {
            get {
                if (instance == null) {
                    instance = new DataManagementService();
                }
                return instance;
            }
        }



        /**
         * will call ArduinoConnectionService.getMeasurementFromArduino()
         * life cycle means data which were generated in i.e. 1 second
         * call this method as : https://www.youtube.com/watch?v=C5VhaxQWcpE from UI
         */
        public ArduinoData getOneLifeCycleOfArduinoData(){
            ArduinoData arduinoData = new ArduinoData();
            // TODO - Edo
            // this method will take 1-2-3 etc SECONDS, 
            // waiting specifing amount of data from ArduinoConnectionService.getMeasurementFromArduino()
            Thread.Sleep(3000);

            return arduinoData;
        }


        /**
         * from arduinoMeasurements will make a :  aggregatedData = new double[arduinoData.length]
         * and create average data and save to into this.aggregatedData .
         * 
         * arduinoData.cycles - tells how many data has to be averaged
         * arduinoData.arduinoMeasurements.validData - tells how many data from arduinoData.measurement are valid (unvalid data exceeded 20miliseconds)
         */
        public AggregatedData getAggregatedData(ArduinoData arduinoData) {
            AggregatedData aggregatedData = new AggregatedData();
            // TODO - Andrej



            return aggregatedData;
        }



        /*
        * Mobilitne zobrazenie je vhodnejsie na identifikaciu ionov. Vztah medzi driftovym casom a pohyblivostou je jednoduchy vzorec. 
        * potom je mozne vypocitat redukovanu pohyblivost ioniov. Vzorec je:
        * Ko=(L^2/U*t)[(p*To)/(po*T))
        * Kde po je normálny tlak (101325 Pa), To je 293.15 K.
        * 
        * Mobility may be applied on aggregated data, mobility is one life cycle of measurement 
        */
        public double[] calculateMobilities(double[] aggregatedData) {
            double[] mobilityData = new double[aggregatedData.Length];

            double Pa = 100.0 * Mobility.p;//Pa <- mbar
            double kV = 1000.0 * Mobility.U; //V <- kV

            for (int i = 0; i < aggregatedData.Length; i++) {
                double t = (i + 0.5) * Settings.sampling / 1000.0e3;//milliseconds       
                var K0 = (Mobility.L * Mobility.L / (kV * t)) * (Pa * 293.15 / (101325.0 * Mobility.T));
                mobilityData[i] = K0;
            }

            return mobilityData;
        }

    }
}
