using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arduin.Backend.Model;

namespace Arduin.Backend
{
    // SINGLETON , use as FileService.instance.XXXX !!!
    class FileService
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

        private const string SETTINGS_PATH = "Data/Settings_Data/";  // path where settings will be save 

        private const string AGGREGATED_DATA_PATH = "Data/Aggregated_Data/";  // path where aggregated data will be save 

        private const string MOBILITY_DATA_PATH = "Data/Moblity_Data/";  // path where mobility data will be save 

        private const string INTENSITY_PATH = "Data/Intensity_Data/";  // path where intensity graph will be save 

        /**
         * will save all setting from class Settings into csv like :
         * repeatSeconds : Settings.instance.repeatSeconds ,
         * repeatCycles :  Settings.instance.repeatCycles,
         * etc....
         */
        public bool saveSettings() {
            // TODO !!!!!
            return false;
        }

        /**
         * will load settings, will inicialise all attribues in Settings.X = N 
         */
        public bool loadSettings() {
            // TODO !!!!!
            return false;
        }



        /**
         * aggregated data is an average of data which was measured in one life cycle, 
         * i.e. one life cycle is around 1 seconds and one measurement
         * may be around 20 miliseconds, so aggregatedData is an average data of 50 measurements 
         * 
         * CSV header must contains : 
         * 1. #Aggregated data <-- Title
         * 2. settings from  Settings --> repeatSeconds : value , repeatCycles : value , etc.... 
         *          2.1 sampling MUST be from aggregatedData.sampling !
         * 3. t(ms), signal(%full range)
         * 4. Data.....
         * 
         * aggregatedData - data which has to be saved
         * specificPath - dateTime (specific date which files has been created) + random name of csv (i.e. : 1.csv , 2.csv , etc.)
         * appliedMobility - if true, put them in folder AGGREGATED_DATA_PATH + currentDaty 
         *                  else MOBILITY_PATH + currentDaty + "/" 
         */
        public bool saveAggregatedData(AggregatedData aggregatedData, string specificPath, bool appliedMobility = false){
            // TODO !!!!!
            return false;
        }




        /**
         * will load data from AGGREGATED_DATA_PATH + specificPath ,
         * specificPath = DateTime (which data was a folder created ) + CSV (specific csv picked by user)
         * data loaded from csv has to be added into arduinoData.aggregatedData
         */
        public AggregatedData loadAggregatedData(string specificPath){
            AggregatedData aggregatedData = new AggregatedData();
            // must load data , sampling and path into aggregatedData
            // TODO !!!!!



            return aggregatedData;
        }


        /**
         * this CSV will be different than CSV for aggregated data.
         * This CSV will contain PATH to the aggegated data which will be then loaded
         * 
         * loop though all intensityData.intensityData , and for each intensityData save
         * its path into the generated CSV
         */
        public bool saveIntensityData(IntensityData intensityData, string specificPath) {
            // TODO !!!!!
            return false;
        }


        /**
         * read the for all aggregated data its path and load their data with this.loadAggregatedData(path) 
         * into IntensityData.intensityData
         */
        public IntensityData loadIntensityData( string specificPath){
            IntensityData intensityData = new IntensityData();
            // TODO !!!!!



            return intensityData;
        }


    }
}
