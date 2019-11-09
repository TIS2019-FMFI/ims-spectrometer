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
         * collect measurements for some time
         * call this method as : https://www.youtube.com/watch?v=C5VhaxQWcpE from UI
         * this method may take around 1 second to execute, must be called async
         */
        private async Task<List<Measurement>> getOneLifeCycleOfArduinoData(){
            List<Measurement> measurements = new List<Measurement>();

            if (Settings.applyRepeatSeconds) {
                // get measurement from serial connection for specific seconds
                DateTime start = DateTime.Now;
                while (DateTime.Now.Subtract(start).Seconds <= Settings.repeatSeconds) {
                    measurements.Add(ArduinoConnectionService.Instance.getMeasurementFromArduino());
                }

            } else {
                // get measurement from serial connection Settings.repeatCycles TIMES
                measurements.AddRange(System.Linq.Enumerable.Range(0, Settings.repeatCycles)
                    .Select(_ => ArduinoConnectionService.Instance.getMeasurementFromArduino()).ToList());
            }

            return measurements;
        }


        /**
         * aggregating data from measurements
         */
        public async Task<AggregatedData> getAggregatedData() {
            AggregatedData aggregatedData = new AggregatedData();

            // synchronized waiting for arduino to send back measurements
            List<Measurement> measurements = await getOneLifeCycleOfArduinoData();

            // if there is no data received from arduino, throw exception
            if (!measurements.Any()) {
                throw new ArithmeticException("There were no measurements found to aggregate data.");
            }

            // save how many measurements will be aggregated
            aggregatedData.numberOfMeasurements = measurements.Count;

            // perform data aggregation
           for (int i=0; i < measurements.First().measurement.Length; i++) {
                foreach(Measurement data in measurements) {
                    aggregatedData.aggregatedData[i] += data.measurement[i];
                }
                aggregatedData.aggregatedData[i] /= aggregatedData.numberOfMeasurements;
            }

            return aggregatedData;
        }



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
