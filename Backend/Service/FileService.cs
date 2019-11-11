using System;
using System.Collections.Generic;
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

        private const string SETTINGS_PATH = "Data/Configuration/";  // path where settings will be save 

        private const string AGGREGATED_DATA_PATH = "Data/Aggregated_Data/";  // path where aggregated data will be save 

        private const string INTENSITY_PATH = "Data/Intensity_Data/";  // path where intensity graph will be save 

        /**
         * will save all setting and Mobility
         */
        public bool saveSettings() {
            // TODO !!!!! - check settings_sample
            return false;
        }

        /**
         * will load settings and mobility
         */
        public bool loadSettings() {
            // TODO !!!!!
            return false;
        }



        /**
         * - everything will be save in path : AGGREGATED_DATA_PATH
         * - name of the file will be in format : Settings.projectName_currentTimestamp
         * - X represents time or mobility base on (appliedMobility is true or false)
         * - Y column represents all data in  AggregatedData.aggregatedData
         * - in Header of the file save everithing what is in AggregatedData - numberOfMeasurements , sampling , gate
         */
        public bool saveAggregatedData(AggregatedData aggregatedData){
            // TODO !!!!! - see example in aggregated_sample  - nezalezi ako v CSV budu udaje, len aby si ich vedel ulozit a nacitat
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
