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
        public static float repeatSeconds { get; set; } = 1;


        /**
         * How many cycles should the Arduino measurement last, default value is 60, but applyRepeatSeconds will
         * determine whether repeatSeconds or  repeatCycles to use to aggregate data
         */
        public static int repeatCycles { get; set; } = 60;


        /**
         * boolean which tells whether to use repeatSeconds or repeatCycles to aggregate data
         * if applyRepeatSeconds == true -> repeatSeconds
         * if applyRepeatSeconds == false -> repeatCycles
         * this value should be a checkbox in UI
         */
        public static bool applyRepeatSeconds { get; set; } = true;


        /**
         * values are in microseconds.
         * 1 miliseconds = 10000 microseconds
         * value describes how often should an Arduino make measurement point in spectrometer,
         * so for example arduino will make each 
         * 5 mikroseconds a measurement which will be repeated 4000 times so one cycle will take 20 000 microsends
         * 20 000 = 4000 * 5 
         */
        public static int sampling { get; set; } = 5;


        /**
         * Pulse width which allows ions to move into drift.
         * How long will be the gate open for ions to access drift is calculated by = sampling * gate
         */
        public static int gate { get; set; } = 2;


        /**
         * determine if user wants to apply hadamard transformation for aggregated data to display on the screen
         */
        public static bool hadamard { get; set; } = false;


    }

}
