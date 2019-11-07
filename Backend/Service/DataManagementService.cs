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
                // get measurement from serial connection for specific seconds
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
         * sync await for one lifecycle of measurement.
         * List will contains N number of measurement which has to be averaged into one double[] which will be displayed on the graph
         */
        public async Task<AggregatedData> getAggregatedData() {
            AggregatedData aggregatedData = new AggregatedData();

            // synchronized waiting for arduino to send back measurements
            ArduinoData arduinoData = await getOneLifeCycleOfArduinoData();

            // if there is no data received from arduino, throw exception
            if (!arduinoData.arduinoMeasurements.Any()) {
                throw new ArithmeticException("There were no measurements found to aggregate data.");
            }

            // save how many measurements will be aggregated
            aggregatedData.numberOfMeasurements = arduinoData.arduinoMeasurements.Count;

            // perform data aggregation
           for (int i=0; i < arduinoData.arduinoMeasurements.First().measurement.Length; i++) {
                foreach(Measurement data in arduinoData.arduinoMeasurements) {
                    aggregatedData.aggregatedData[i] += data.measurement[i];
                }
                aggregatedData.aggregatedData[i] /= aggregatedData.numberOfMeasurements;
            }

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
        public double[] calculateMobilities(AggregatedData data) {
            double[] mobilityData = new double[data.aggregatedData.Length];

            double Pa = 100.0 * Mobility.p;//Pa <- mbar
            double kV = 1000.0 * Mobility.U; //V <- kV

            for (int i = 0; i < data.aggregatedData.Length; i++) {
                double t = (i + 0.5) * Settings.sampling / 1000.0e3;//milliseconds       
                var K0 = (Mobility.L * Mobility.L / (kV * t)) * (Pa * 293.15 / (101325.0 * Mobility.T));
                mobilityData[i] = K0;
            }

            return mobilityData;
        }

    }
}
