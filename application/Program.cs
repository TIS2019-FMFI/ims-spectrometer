using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arduin.Backend;
using Arduin.Backend.Model;
namespace Arduin
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
             Application.EnableVisualStyles();
             Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new Form1());
           /* Settings.projectName = "Eduard_Test";
            ArduinoConnectionService.Instance.start();
             Measurement measurement = ArduinoConnectionService.Instance.getMeasurementFromArduino();
              for (int i = 0; i < measurement.measurement.Length; i++) {
                  Console.WriteLine(measurement.measurement[i]);
              }
              Console.WriteLine(measurement.measurement.Length);

            compoareLength().Wait();

             Console.WriteLine("-----------------------------------------------------------");
             Console.WriteLine("length of measurements = ");
             Console.WriteLine(measurement.measurement.Length);
             measurement = ArduinoConnectionService.Instance.getMeasurementFromArduino();
             Console.WriteLine(measurement.measurement.Length);
             measurement = ArduinoConnectionService.Instance.getMeasurementFromArduino();
             Console.WriteLine(measurement.measurement.Length);
             measurement = ArduinoConnectionService.Instance.getMeasurementFromArduino();
             Console.WriteLine(measurement.measurement.Length);

             Console.WriteLine("-----------------------------------------------------------");
             Console.WriteLine("awaiOneLIfeCycle().Wait()");
             awaiOneLIfeCycle().Wait();

             Console.WriteLine("-----------------------------------------------------------");
             Console.WriteLine("awaitGetAggregatedData().Wait()");
             awaitGetAggregatedData().Wait();

             Console.WriteLine("-----------------------------------------------------------");
             Console.WriteLine("awaitIntensityData().Wait()");
             awaitIntensityData().Wait();

             Console.WriteLine("-----------------------------------------------------------");
             Console.WriteLine("Change settings, before");
             makeAgregatedData().Wait();
             Settings.sampling = 2;
             Settings.gate = 35;
             ArduinoConnectionService.Instance.sendSettingsToArduino();
             Console.WriteLine("Change settings, after");
             makeAgregatedData().Wait();



             Console.ReadKey();*/

        }

        private static async Task compoareLength() {
            ArduinoConnectionService.Instance.start();

            Settings.sampling = 5;
            Task.Delay(500).Wait();
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementNormal = await DataManagementService.Instance.getAggregatedData();
            Task.Delay(100).Wait();

            Settings.sampling = 30;
            Task.Delay(500).Wait();
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementSlow = await DataManagementService.Instance.getAggregatedData();
            Task.Delay(100).Wait();

            Settings.sampling = 1;
            Task.Delay(500).Wait();
            ArduinoConnectionService.Instance.sendSettingsToArduino();
            AggregatedData measurementFast = await DataManagementService.Instance.getAggregatedData();
            Task.Delay(100).Wait();

            Console.WriteLine(measurementFast.aggregatedData.Length);
            Console.WriteLine(measurementNormal.aggregatedData.Length);
            Console.WriteLine(measurementSlow.aggregatedData.Length);

            for(int i = 0; i < measurementFast.aggregatedData.Length; i++) {
                Console.WriteLine(measurementFast.aggregatedData[i]);
            }
        }


        private static async Task makeAgregatedData() {
            AggregatedData aggregated1 = await DataManagementService.Instance.getAggregatedData();
            AggregatedData aggregated2 = await DataManagementService.Instance.getAggregatedData();
            AggregatedData aggregated3 = await DataManagementService.Instance.getAggregatedData();
            Console.WriteLine(aggregated1.numberOfMeasurements);
            Console.WriteLine(aggregated2.numberOfMeasurements);
            Console.WriteLine(aggregated3.numberOfMeasurements);
        }


        private static async Task awaitIntensityData() {
            IntensityData intensityData = new IntensityData();
            intensityData.intensityData = new List<AggregatedData>();
            intensityData.intensityData.Add(await DataManagementService.Instance.getAggregatedData());
            intensityData.intensityData.Add(await DataManagementService.Instance.getAggregatedData());
            intensityData.intensityData.Add(await DataManagementService.Instance.getAggregatedData());
            intensityData.intensityData.Add(await DataManagementService.Instance.getAggregatedData());

            Console.WriteLine(intensityData.intensityData.Count);
            FileService.Instance.saveIntensityData(intensityData);
        }


        private static async Task awaitGetAggregatedData() {
            AggregatedData aggregated = await DataManagementService.Instance.getAggregatedData();
            Console.WriteLine(aggregated.aggregatedData.Length);
            Console.WriteLine(aggregated.gate);
            FileService.Instance.saveAggregatedData(aggregated);

        }

        private static async Task awaiOneLIfeCycle() {
            List<Measurement> measurements = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Console.WriteLine("First : " + measurements.Count);

            measurements = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Console.WriteLine("SECOND : " + measurements.Count);

            measurements = await DataManagementService.Instance.getOneLifeCycleOfArduinoData();
            Console.WriteLine("THIRD : " + measurements.Count);

        }
    }
}
