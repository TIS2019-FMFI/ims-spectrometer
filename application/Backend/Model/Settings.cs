using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model{

    /**
     * Setting which are displayed on UI and used to control arduino or aggregate data received from arduino,
     * use as Settings.X
     */
    public sealed class Settings {

        /**
         * How many seconds should one life cycle of measurement last, default values if 1 second, 
         * after that, all measured values need to be aggregated 
         */
        public static float repeatSeconds { get; set; } = 2;


        /**
         * How many cycles should the Arduino measurement last, default value is 60, but applyRepeatSeconds will
         * determine whether repeatSeconds or  repeatCycles to use to aggregate data
         */
        public static int repeatCycles { get; set; } = 60;


        /**
         * boolean which tells whether to use repeatSeconds or repeatCycles to aggregate data
         * if applyRepeatSeconds == true -> repeatSeconds
         * if applyRepeatSeconds == false -> repeatCycles
         * this value should be a check box in UI
         */
        public static bool applyRepeatCount { get; set; } = false;


        /**
         * values are in microseconds. Space between points of measurement 
         */
        public static int sampling { get; set; } = 5;


        /**
         * Pulse width which allows ions to move into drift.
         */
        public static int gate { get; set; } = 2;


        /**
         * Name of the project defined by User
         */
        public static string projectName { get; set; } = "Project_name";

    }

}
