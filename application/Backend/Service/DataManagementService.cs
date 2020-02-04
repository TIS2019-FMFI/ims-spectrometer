using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduin.Backend.Model;
using System.Threading;
using System.Diagnostics;

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

            if (Settings.applyRepeatCount) {
                // get measurement from serial connection Settings.repeatCycles TIMES
                measurements.AddRange(System.Linq.Enumerable.Range(0, Settings.repeatCycles)
                    .Select(_ => ArduinoConnectionService.Instance.getMeasurementFromArduino()).ToList());
            } else {
                // get measurement from serial connection for specific seconds
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed <= TimeSpan.FromSeconds(Settings.repeatSeconds)) {
                    measurements.Add(ArduinoConnectionService.Instance.getMeasurementFromArduino());
                }
                s.Stop();
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


        /*
         * Mobilitne zobrazenie je vhodnejsie na identifikaciu ionov. Vztah medzi driftovym casom a pohyblivostou je jednoduchy vzorec. 
         * Kde treba zadat niekolko parametrov merania 
         * (dlzku trubice-L(cm), 
         * tlak plynu - p- (pa), 
         * teplotu plynu T(K), 
         * napatie na driftovej trubici U (kV))  (mohli by sa zadavat ) 
         * a samozrejme driftovy cas -t (ms) 
         * potom je mozne vypocitat redukovanu pohyblivost ioniov. Vzorec je:
         * Ko=(L^2/U*t)[(p*To)/(po*T))
         * Kde po je normálny tlak (101325 Pa), To je 293.15 K, 
         */
        public double[] calculateMobilities(AggregatedData data) {
            double[] mobilityData = new double[10]; // data.aggregatedData.Length

            double Pa = 100.0 * Mobility.p;//Pa <- mbar
            double kV = 1000.0 * Mobility.U; //V <- kV

            for (int i = 0; i < 10; i++) {
                double t = (i + 0.5) * Settings.sampling / 1000.0e3;//milliseconds       
                var K0 = (Mobility.L * Mobility.L / (kV * t)) * (Pa * 293.15 / (101325.0 * Mobility.T));
                mobilityData[i] = K0;
            }

            return mobilityData;
        }

    }
}
