using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduin.Backend;
using Arduin.Backend.Model;

namespace ArduinTest {
        [TestClass]
        public class FileServiceTest {
            [TestMethod]
            public void saveAndLoadSettings() {
                Settings.projectName = "TestProject";

                // save variable
                int gate = Settings.gate;
                int sampling = Settings.sampling;
                float repeatSeconds = Settings.repeatSeconds;
                int repeatCycles = Settings.repeatCycles;
                bool applyRepeatSeconds = Settings.applyRepeatSeconds;
                string projectName = Settings.projectName;

                double l = Mobility.L;
                double p = Mobility.p;
                double u = Mobility.U;
                double t = Mobility.T;

                // save current settings into csv
                string output = FileService.Instance.saveSettingsAndMobility();

                // overwrite variable
                Mobility.T = 0;
                Mobility.U = 0;
                Mobility.p = 0;
                Mobility.L = 0;
                Settings.applyRepeatSeconds = false;
                Settings.gate = 0;
                Settings.projectName = "abc";
                Settings.repeatCycles = 0;
                Settings.repeatSeconds = 0;
                Settings.sampling = 0;

                // load settings back
                FileService.Instance.loadSettingsAndMobility(output);

                // test of new loaded are equal to saved 
                Assert.AreEqual(Mobility.T, t);
                Assert.AreEqual(Mobility.U, u);
                Assert.AreEqual(Mobility.p, p);
                Assert.AreEqual(Mobility.L, l);
                Assert.AreEqual(Settings.projectName, projectName);
                Assert.AreEqual(Settings.gate, gate);
                Assert.AreEqual(Settings.applyRepeatSeconds, applyRepeatSeconds);
                Assert.AreEqual(Settings.repeatCycles, repeatCycles);
                Assert.AreEqual(Settings.repeatSeconds, repeatSeconds);
                Assert.AreEqual(Settings.sampling, sampling);
            }

            [TestMethod]
            public void saveAggregatedData() {
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

                string output = Settings.projectName + "_" + DateTime.Now.ToString("dd/MM/yyyy");

                Assert.AreEqual(FileService.Instance.saveAggregatedData(aggregatedTest), output);
            }


            [TestMethod]
            public void saveAndLoadIntensityData() {
                IntensityData currentIntensityData = constructIntensityData();

                String output = FileService.Instance.saveIntensityData(currentIntensityData);
                IntensityData loadedIntensityData = FileService.Instance.loadIntensityData(output);

                Assert.AreEqual(loadedIntensityData.intensityData.Count, currentIntensityData.intensityData.Count);
                Assert.AreEqual(loadedIntensityData.intensityData[0].aggregatedData.Length, currentIntensityData.intensityData[0].aggregatedData.Length);
                Assert.AreEqual(loadedIntensityData.intensityData[0].gate, currentIntensityData.intensityData[0].gate);
                Assert.AreEqual(loadedIntensityData.intensityData[0].sampling, currentIntensityData.intensityData[0].sampling);
                Assert.AreEqual(loadedIntensityData.intensityData[0].numberOfMeasurements, currentIntensityData.intensityData[0].numberOfMeasurements);

                for (int i = 0; i < currentIntensityData.intensityData.Count; i++) {
                    for (int j = 0; j < currentIntensityData.intensityData[i].aggregatedData.Length; j++) {
                        Assert.AreEqual(loadedIntensityData.intensityData[i].aggregatedData[j], currentIntensityData.intensityData[i].aggregatedData[j]);
                    }
                }
            }



            private IntensityData constructIntensityData() {
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

                aggregated1.numberOfMeasurements = 2;
                aggregated1.aggregatedData[0] = 2;
                aggregated1.aggregatedData[1] = 2;
                aggregated1.aggregatedData[2] = 5;
                aggregated1.aggregatedData[3] = 5;
                aggregated1.aggregatedData[4] = 5;
                aggregated1.aggregatedData[5] = 2;
                aggregated1.aggregatedData[6] = 2;
                aggregated1.aggregatedData[7] = 2;
                aggregated1.aggregatedData[8] = 2;


                AggregatedData aggregated2 = new AggregatedData();
                aggregated2.aggregatedData = new int[9];

                aggregated2.numberOfMeasurements = 4;
                aggregated2.aggregatedData[0] = 4;
                aggregated2.aggregatedData[1] = 4;
                aggregated2.aggregatedData[2] = 4;
                aggregated2.aggregatedData[3] = 4;
                aggregated2.aggregatedData[4] = 10;
                aggregated2.aggregatedData[5] = 10;
                aggregated2.aggregatedData[6] = 10;
                aggregated2.aggregatedData[7] = 4;
                aggregated2.aggregatedData[8] = 4;

                IntensityData currentIntensityData = new IntensityData();
                currentIntensityData.intensityData.Add(aggregated);
                currentIntensityData.intensityData.Add(aggregated1);
                currentIntensityData.intensityData.Add(aggregated2);

                return currentIntensityData;
            }
        }
    }

