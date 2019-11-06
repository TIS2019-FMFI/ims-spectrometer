using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduin.Backend.Model;
using System.Threading;

namespace Arduin.Backend{
    // SINGLETON , use as DataManagementService.instance.XXXX !!!
    public class DataManagementService {
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
         * this method may take around 1 second to execute, must be called async
         */
        private async Task<ArduinoData> getOneLifeCycleOfArduinoData(){
            ArduinoData arduinoData = new ArduinoData();

            if (Settings.applyRepeatSeconds) {
                // get measurement from serial connection for spexific seconds
                DateTime start = DateTime.Now;
                while (DateTime.Now.Subtract(start).Seconds <= Settings.repeatSeconds) {
                    arduinoData.arduinoMeasurements.Add(ArduinoConnectionService.Instance.getMeasurementFromArduino());
                }

            } else {
                // get measurement from serial connection Settings.repeatCycles TIMES
                arduinoData.arduinoMeasurements.AddRange(System.Linq.Enumerable.Range(0, Settings.repeatCycles)
                    .Select(_ => ArduinoConnectionService.Instance.getMeasurementFromArduino()).ToList());
            }

            return arduinoData;
        }


        /**
         * from arduinoMeasurements will make a :  aggregatedData = new double[arduinoData.length]
         * and create average data and save to into this.aggregatedData .
         * 
         * arduinoData.cycles - tells how many data has to be averaged
         * arduinoData.arduinoMeasurements.validData - tells how many data from arduinoData.measurement are valid (unvalid data exceeded 20miliseconds)
         */
        public async Task<AggregatedData> getAggregatedData() {
            AggregatedData aggregatedData = new AggregatedData();
            ArduinoData arduinoData = await getOneLifeCycleOfArduinoData();

            // TODO - Andrej - vytvor agregaciu dat z arduinoData do aggregatedData
            // musis vyplnit v aggregatedData.number - to je arduinoData.arduinoMeasurements.size()
            //  aggregatedData.sampling - aktualny sampling Sitting.sampling
            // aggregatedData.aggregatedData - priemer dat



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
