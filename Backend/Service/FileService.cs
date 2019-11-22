using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduin.Backend.Model;

namespace Arduin.Backend
{
    // SINGLETON , use as FileService.instance.XXXX !!!
    public class FileService
    {
        // SINGLETON , use as FileService.instance.XXXX !!!
        private static FileService instance;

        private FileService() { }

        public static FileService Instance{
            get {
                if (instance == null){
                    instance = new FileService();
                }
                return instance;
            }
        }

        private const string SETTINGS_PATH = "Data\\Configuration\\";  // path where settings will be save 

        private const string AGGREGATED_DATA_PATH = "Data\\Agregated_Data\\";  // path where aggregated data will be saved

        private const string INTENSITY_PATH = "Data\\Intensity_Data\\";  // path where intensity graph will be saved 

        private void createFolderIfNotExists(string path) {
            if (!System.IO.Directory.Exists(path)) {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /**
         * will save all setting and Mobility
         */
        public bool saveSettingsAndMobility() {
            try {
                this.createFolderIfNotExists(AppDomain.CurrentDomain.BaseDirectory + SETTINGS_PATH);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + SETTINGS_PATH + Settings.projectName + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".txt";
                using (StreamWriter sw = File.CreateText(fileName)) {
                    // save settings
                    sw.WriteLine("repeatSeconds :" + Settings.repeatSeconds);
                    sw.WriteLine("repeatCycles :" + Settings.repeatCycles);
                    sw.WriteLine("applyRepeatSeconds :" + Settings.applyRepeatSeconds);
                    sw.WriteLine("sampling :" + Settings.sampling);
                    sw.WriteLine("gate :" + Settings.gate);
                    // save mobility
                    sw.WriteLine("Mobility - L :" + Mobility.L);
                    sw.WriteLine("Mobility - p :" + Mobility.p);
                    sw.WriteLine("Mobility - T :" + Mobility.T);
                    sw.WriteLine("Mobility - U :" + Mobility.U);
                    sw.Close();
                }
            }catch(Exception e) {
                throw new Exception("Could not save current settings, exception : " + e.Message);
            }
            return true;
        }

     
        public bool loadSettingsAndMobility(String projectName) {
            string[] lines;
            var list = new List<string>();

            try {
                using (var streamReader = tryToOpenFile(projectName)) {
                    string line;
                    while ((line = streamReader.ReadLine()) != null) {
                        list.Add(line.Split(':')[1]);
                    }
                }
                
                lines = list.ToArray();
                Settings.repeatSeconds = float.Parse(lines[0]);
                Settings.repeatCycles = Int32.Parse(lines[1]);
                Settings.applyRepeatSeconds = bool.Parse(lines[2]);
                Settings.sampling = Int32.Parse(lines[3]);
                Settings.gate = Int32.Parse(lines[4]);

                Mobility.L = Double.Parse(lines[5]);
                Mobility.p = Double.Parse(lines[6]);
                Mobility.T = Double.Parse(lines[7]);
                Mobility.U = Double.Parse(lines[8]);
            } catch(Exception e) {
                throw new FileLoadException("Could not parse file content into setting variables, got error : " + e.Message);
            }
         
            return true;
        }



        
        public bool saveAggregatedData(AggregatedData aggregatedData){
            try {
                this.createFolderIfNotExists(AppDomain.CurrentDomain.BaseDirectory + AGGREGATED_DATA_PATH);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + AGGREGATED_DATA_PATH + Settings.projectName + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".csv";
                using (StreamWriter sw = File.CreateText(fileName)) {
                    sw.WriteLine("#HEADER");
                    sw.WriteLine("numberOfMeasurements :" + aggregatedData.numberOfMeasurements);
                    sw.WriteLine("sampling :" + aggregatedData.sampling);
                    sw.WriteLine("gate :" + aggregatedData.gate);

   
                    double sampling = 0;
                    sw.WriteLine("X(ms); Y1");
                    for (int i = 0; i <  aggregatedData.aggregatedData.Length; i++) {
                        sw.WriteLine(String.Format("{0:0.000}", sampling) + ";" + aggregatedData.aggregatedData[i]);

                        sampling += ((double) aggregatedData.sampling / 1000);
                    }


                    sw.Close();
                }
            } catch (Exception e) {
                throw new Exception("Could not save AggregatedData, exception : " + e.Message);
            }
            return true;
        }


        public bool saveIntensityData(IntensityData intensityData) {
            int numberofElements = intensityData.intensityData.Count;
            if (numberofElements == 0) {
                throw new Exception("Could not save IntensityData, does not contains any data ");
            }
            try {
                this.createFolderIfNotExists(AppDomain.CurrentDomain.BaseDirectory + INTENSITY_PATH);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + INTENSITY_PATH + Settings.projectName + "_" + DateTime.Now.ToString("dd/MM/yyyy") + ".csv";
                using (StreamWriter sw = File.CreateText(fileName)) {
                    AggregatedData firstAggregatedData = intensityData.intensityData.First();
                    sw.WriteLine("#HEADER");
                    sw.WriteLine("numberOfAggregatedData  :" + numberofElements);
                    sw.WriteLine("lengthOfOneAggregatedData  :" + firstAggregatedData.aggregatedData.Length);
                    sw.WriteLine("numberOfMeasurements :" + firstAggregatedData.numberOfMeasurements);
                    sw.WriteLine("sampling :" + firstAggregatedData.sampling);
                    sw.WriteLine("gate :" + firstAggregatedData.gate);

                    // create header for csv
                    string header = "t(ms);";
                    for(int i = 1; i <= numberofElements; i++) {
                        header += "Y" + i + ";";
                    }
                    header = header.Remove(header.Length - 1); // remove last comma
                    sw.WriteLine(header);

                    // append data into csv
                    double sampling = 0;
                    string line = "";
                    for (int i = 0; i < firstAggregatedData.aggregatedData.Length; i++) {
                        line = String.Format("{0:0.000}", sampling);
                        for (int j = 0; j < numberofElements; j++) {
                            line += ";" + intensityData.intensityData[j].aggregatedData[i] ;
                        }
                        sw.WriteLine(line);
                        sampling += ((double)firstAggregatedData.sampling / 1000);
                    }

                    sw.Close();
                }
            } catch (Exception e) {
                throw new Exception("Could not save IntensityData, exception : " + e.Message);
            }
            return true;
        }


        private StreamReader tryToOpenFile(string projectName) {
            FileStream fileStream = null;
            try {
                fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + INTENSITY_PATH + projectName, FileMode.Open, FileAccess.Read);
            } catch (Exception e) {
                throw new FileNotFoundException("Could not find file : " + projectName + " got exception : " + e.Message);
            }
            return new StreamReader(fileStream, Encoding.UTF8);
        }

        public IntensityData loadIntensityData(string projectName) {
            IntensityData intensityData = new IntensityData();

            try {
                using (var streamReader = tryToOpenFile(projectName)) {
                    string line = "";
                    int countline = 0;
                    int sampling = 0;
                    int gate = 0;
                    int numberOfMeasurements = 0;
                    int lengthOfOneMeasuremet = 0;
                    int numberOfAggregatedData = 0;
                    List<string[]> saveMeasurements = new List<string[]>();
                    
                    while ((line = streamReader.ReadLine()) != null) {
                        if (countline == 1) {
                            numberOfAggregatedData = Int32.Parse(line.Split(':')[1]);
                        } else if (countline == 2) {
                            lengthOfOneMeasuremet = Int32.Parse(line.Split(':')[1]);
                        } else if (countline == 3) {
                            numberOfMeasurements = Int32.Parse(line.Split(':')[1]);
                        } else if (countline == 4) {
                            sampling = Int32.Parse(line.Split(':')[1]);
                        } else if (countline == 5) {
                            gate = Int32.Parse(line.Split(':')[1]);
                        } else if (countline == 0 || countline == 6) {
                            countline++;
                            continue;
                        } else {
                            saveMeasurements.Add(line.Split(';'));
                        }
                        
                        countline++;
                    }

                    // create instance of aggregated data into intensity data
                    for (int i = 0; i < numberOfAggregatedData; i++) {
                         AggregatedData aggregated = new AggregatedData();
                         aggregated.sampling = sampling;
                         aggregated.gate = gate;
                         aggregated.numberOfMeasurements = numberOfMeasurements;
                         aggregated.aggregatedData = new int[lengthOfOneMeasuremet];

                         intensityData.intensityData.Add(aggregated);
                     }

                     // save data from csv into list of aggregated data
                     int positionCounter = 0;
                     foreach(string[] measurement in saveMeasurements){
                         for(int i = 0; i < numberOfAggregatedData; i++) {
                             intensityData.intensityData[i].aggregatedData[positionCounter] = Int32.Parse(measurement[i + 1]);
                         }
                         positionCounter++;
                     }
                }


            } catch (Exception e) {
                throw new FileLoadException("Could not parse file content into IntensityData, got error : " + e.Message);
            }


            return intensityData;
        }


    }
}
