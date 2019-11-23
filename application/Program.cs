using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arduin.Backend;
using Arduin.Backend.Model;
namespace Arduin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            FileService.Instance.saveSettingsAndMobility();
            Console.WriteLine(Settings.gate);
            Console.WriteLine(Mobility.L);
            Settings.gate = 18;
            Mobility.L = 100;
            Console.WriteLine("--------------");
            Console.WriteLine(Settings.gate);
            Console.WriteLine(Mobility.L);
            Console.WriteLine("--------------");
            FileService.Instance.loadSettingsAndMobility("undefined_23. 11. 2019");
            Console.WriteLine(Settings.gate);
            Console.WriteLine(Mobility.L);

            // aggregated data
              AggregatedData aggregatedTest = new AggregatedData();
            aggregatedTest.aggregatedData = new int[9];

            aggregatedTest.numberOfMeasurements = 5;
            aggregatedTest.aggregatedData[0] = 5;
            aggregatedTest.aggregatedData[1] = 6;
            aggregatedTest.aggregatedData[2] = 5;
            aggregatedTest.aggregatedData[3] = 5;
            aggregatedTest.aggregatedData[4] = 10;
            aggregatedTest.aggregatedData[5] = 11;
            aggregatedTest.aggregatedData[6] = 15;
            aggregatedTest.aggregatedData[7] = 6;
            aggregatedTest.aggregatedData[8] = 5;

              FileService.Instance.saveAggregatedData(aggregatedTest);



            // save intensity data
             AggregatedData aggregated = new AggregatedData();
             aggregated.aggregatedData = new int[9];

             aggregated.numberOfMeasurements = 5;
             aggregated.aggregatedData[0] = 5;
             aggregated.aggregatedData[1] = 6;
             aggregated.aggregatedData[2] = 5;
             aggregated.aggregatedData[3] = 5;
             aggregated.aggregatedData[4] = 10;
             aggregated.aggregatedData[5] = 11;
             aggregated.aggregatedData[6] = 15;
             aggregated.aggregatedData[7] = 6;
             aggregated.aggregatedData[8] = 5;


             AggregatedData aggregated1 = new AggregatedData();
             aggregated1.aggregatedData = new int[9];

             aggregated1.numberOfMeasurements = 5;
             aggregated1.aggregatedData[0] = 5;
             aggregated1.aggregatedData[1] = 6;
             aggregated1.aggregatedData[2] = 5;
             aggregated1.aggregatedData[3] = 5;
             aggregated1.aggregatedData[4] = 10;
             aggregated1.aggregatedData[5] = 11;
             aggregated1.aggregatedData[6] = 15;
             aggregated1.aggregatedData[7] = 6;
             aggregated1.aggregatedData[8] = 5;


             AggregatedData aggregated2 = new AggregatedData();
             aggregated2.aggregatedData = new int[9];

             aggregated2.numberOfMeasurements = 5;
             aggregated2.aggregatedData[0] = 5;
             aggregated2.aggregatedData[1] = 6;
             aggregated2.aggregatedData[2] = 5;
             aggregated2.aggregatedData[3] = 5;
             aggregated2.aggregatedData[4] = 10;
             aggregated2.aggregatedData[5] = 11;
             aggregated2.aggregatedData[6] = 15;
             aggregated2.aggregatedData[7] = 6;
             aggregated2.aggregatedData[8] = 5;

             IntensityData intensityData = new IntensityData();
             intensityData.intensityData.Add(aggregated);
             intensityData.intensityData.Add(aggregated1);
             intensityData.intensityData.Add(aggregated2);

             FileService.Instance.saveIntensityData(intensityData);

            IntensityData intensity = FileService.Instance.loadIntensityData("undefined_23. 11. 2019");
            Console.WriteLine(intensity.intensityData.Count);
            for(int i = 0; i < intensity.intensityData.First().aggregatedData.Length; i++) {
                Console.WriteLine(intensity.intensityData.First().aggregatedData[i]);
            }

        }
    }
}
