using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model
{
    class AggregatedData
    {
        /**
         * path where a corresponding csv file is located for this AggregatedData
         */
        public string path { get; set; } = "";

        /**
         * number tells how far are each data from aggregatedData to each other.
         * This sampling is usefull when data from aggregatedData will be display on Aggregated GRAPH
         */
        public int sampling { get; set; } = Settings.sampling;

        /**
        *  After one lifecycle (i.e. 1 second), arduinoMeasurements will contain X measurements and 
        *  those data has to be averaged into this attribute
        */
        public double[] aggregatedData { get; private set; }


        
    }
}
