using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Arduin.Backend.Model{

    /**
     * One measurement , lasting around 20 000 microseconds
     */
    class Measurement{
        /**
         * container for measured data by arduino, maximum 4095 points
         * hadamard encoding, but may be less if time exceed 20 000 microseconds.
         * How many numbers of measurement should be valid will tell validData, only those will be displayed on graph
         */
        public double[] measurement { get; set; } = new double[4095];

        /**
         * tells how many data from measurement are valid and will be displayed on graph.
         * May happen that from measurement only the first 1500 data are some acctualy value, rest will be 0 (will not be presented on graph)
         * usualy it will be 4095, but may be less (never more)
         */
        public int validData { get; set; } = 0;
    }





    /**
     * all data received from arduino will be collected here.
     * This class is used to make and average data from all collected measurements (which will be measured every 20 miliseconds)
     * from one life cycle (i.e 1 seconds), so list may contains around 50 measurements from which has to be make an average
     */
    class ArduinoData{


        // container for measured data from arduino by one cycle
        public List<Measurement> arduinoMeasurements { get; set; } = new List<Measurement>();       

    }
}
