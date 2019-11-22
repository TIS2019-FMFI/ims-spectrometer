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

        private const string AGGREGATED_DATA_PATH = "Data\\ggregated_Data\\";  // path where aggregated data will be saved

        private const string INTENSITY_PATH = "Data\\Intensity_Data\\";  // path where intensity graph will be saved 

        private void createFolderIfNotExists(string path) {
            if (!System.IO.Directory.Exists(path)) {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /**
         * will save all setting and Mobility
         */
        public bool saveSettings() {
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

        /**
         * will load settings and mobility
         */
        public bool loadSettings(String projectName) {
            string[] lines;
            var list = new List<string>();
            FileStream fileStream = null;
            try {
                 fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + SETTINGS_PATH + projectName + ".txt", FileMode.Open, FileAccess.Read);
            }catch(Exception e) {
                throw new FileNotFoundException("Could not find file : " + projectName + " got exception : " + e.Message);
            }

            try {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
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



        /**
         * - everything will be save in path : AGGREGATED_DATA_PATH
         * - name of the file will be in format : Settings.projectName_currentTimestamp
         * - X represents time or mobility base on (appliedMobility is true or false)
         * - Y column represents all data in  AggregatedData.aggregatedData
         * - in Header of the file save everithing what is in AggregatedData - numberOfMeasurements , sampling , gate
         */
        public bool saveAggregatedData(AggregatedData aggregatedData){
            return false;
        }



        /**
         * for each AggregatedData in the list will be save as a new column.
         * First column X represents time
         * each Y in column represents an aggregated data, so if Intensity data contains
         * 50 aggregated data, there will be Y1, Y2....Y50 in csv file
         * name of the file will be in format : Settings.projectName_currentTimestamp
         */
        public bool saveIntensityData(IntensityData intensityData) {
            // check sample - intensity_data_sample
            return false;
        }


        /**
         * specificPath - should be defined by user
         */
        public IntensityData loadIntensityData(string specificPath){
            IntensityData intensityData = new IntensityData();
            // TODO !!!!!



            return intensityData;
        }


    }
}
