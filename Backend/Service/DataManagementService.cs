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
        public async Task<List<Measurement>> getOneLifeCycleOfArduinoData(){
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


            int[] sum = new int[Measurement.BUFFER_SIZE];
            int maximumSize = 0;

            // sum of all measurement in each position,
           foreach(Measurement measurement in measurements) {
                int measurementSize = measurement.measurement.Length;

                if(maximumSize < measurementSize) {
                    maximumSize = measurementSize;
                }

                for (int i = 0; i < measurementSize; i++) {
                    sum[i] += measurement.measurement[i];
                }
            }

            // perform data average
            for (int i = 0; i < maximumSize; i++) {
                sum[i] /= aggregatedData.numberOfMeasurements;
            }

            // copy values from averaged sum into returning object
            aggregatedData.aggregatedData = new int[maximumSize];
            Array.Copy(sum, aggregatedData.aggregatedData, maximumSize);

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
