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
        public double[] measurement { get; set; } = new double[4095];

        /**
         * tells how many data from measurement are valid and will be displayed on graph,
         * usualy it will be 4095, but may be less (never more)
         */
        public int validData { get; set; } = 4095;
    }





    /**
     * all data received from arduino will be collected here.
     * This class is used to make and average data from all collected measurements (which will be measured every 20 miliseconds)
     * from one life cycle (i.e 1 seconds), so list may contains around 50 measurements from which has to be make an average
     */
    class ArduinoData{


        /**
         * will contain measured data from arduino by one cycle. One cycle will be usualy 4095 points
         * hadamard encoding, but may less if time would exceed 20 000 microseconds.
         * So for example :
         *          sampling = 20
         *          points = 4095
         *          time = sampling * points = 4095 * 20 -> 80 000+
         *          so in this case only the first 1000 data from measurement will be valid, rest will be 0, becasue
         *          20 000 / 20 = 1 000
         */
        public List<Measurement> arduinoMeasurements { get; set; } = new List<Measurement>();


        /**
         * number which tells how many arduinoMeasurements has been done
         * so acctually arduinoMeasurements.size() == cycles
         * this number will help to get the average data from arduinoMeasurements
         */
        public int cycles { get; set; } = 0;

       

    }
}
