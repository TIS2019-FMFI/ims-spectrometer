using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduin.Backend;
using Arduin.Backend.Model;
using System.Threading.Tasks;

namespace ArduinTest {
    [TestClass]
    public class DataManagerTest {
        [TestMethod]
        public void testConnectionAndDataRetrieving() {
            ArduinoConnectionService.Instance.start();
            Measurement measurement = ArduinoConnectionService.Instance.getMeasurementFromArduino();
            Assert.IsTrue(measurement.measurement.Length > 0, "Measured data container is empty");
            Assert.IsTrue(measurement.measurement[0] > 0, "Data on first index was 0");
        }

        /**
         * Test if measured data was greater than 0, container cannot contain unused values
         */
        [TestMethod]
        public async Task testGetOneLifeCycleOfArduinoData() {
            ArduinoConnectionService.Instance.start();
            List<Measurement> measurements = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Assert.IsTrue(measurements.Count > 0, "lsit of emasurements was 0");
            foreach(Measurement measurement in measurements) {
                for(int i = 0; i < measurement.measurement.Length; i++) {
                    Assert.IsTrue(measurement.measurement[0] > 0);
                }
            }
        }

        /**
         * Test agregated data if there were more than 0 measurements and average of data is > 0
         */
        [TestMethod]
        public async Task testGetAggregatedData() {
            ArduinoConnectionService.Instance.start();
            AggregatedData aggregated = await DataManagementService.Instance.getAggregatedData();
            Assert.IsTrue(aggregated.aggregatedData.Length > 0, "container for agregated data is empty");
            Assert.IsTrue(aggregated.numberOfMeasurements > 0 , "number of measurements for agregated data is 0");
            for(int i=0; i < aggregated.aggregatedData.Length; i++) {
                Assert.IsTrue(aggregated.aggregatedData[i] > 0);
            }
        }

        [TestMethod]
        public async Task testChangingSettings() {
            ArduinoConnectionService.Instance.start();

            Settings.sampling = 5;
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementNormal = await DataManagementService.Instance.getAggregatedData();

            Settings.sampling = 30;
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementSlow = await DataManagementService.Instance.getAggregatedData();

            Settings.sampling = 1;
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementFast = await DataManagementService.Instance.getAggregatedData();

            Console.WriteLine(measurementFast.aggregatedData.Length);
            Console.WriteLine(measurementNormal.aggregatedData.Length);
            Console.WriteLine(measurementSlow.aggregatedData.Length);

            Assert.IsTrue(measurementFast.aggregatedData.Length > measurementNormal.aggregatedData.Length);
            Assert.IsTrue(measurementFast.aggregatedData.Length > measurementSlow.aggregatedData.Length);
            Assert.IsTrue(measurementNormal.aggregatedData.Length > measurementSlow.aggregatedData.Length);

        }

        [TestMethod]
        public async Task testChangingSecondsToNumberForMeasuring() {
            ArduinoConnectionService.Instance.start();

            List<Measurement> measurements1 = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Settings.applyRepeatCount = false;
            Settings.repeatCycles = 100;
            List<Measurement> measurements2 = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Assert.IsTrue(measurements2.Count == Settings.repeatCycles);
            Assert.IsTrue(measurements2.Count > measurements1.Count);
        }

    }
}
