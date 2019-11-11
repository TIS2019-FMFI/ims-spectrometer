using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model
{
    public class AggregatedData{

        /**
         * tells how many measurements was averaged to create this data structure
         */
        public int numberOfMeasurements { get; set; } = 0;

        /**
         * sampling from current measurement - will be display on Aggregated or mirror graph
         */
        public int sampling { get; private set; } = Settings.sampling;

        /**
         * gate from current measurement - - will be display on Aggregated or mirror graph
         */
        public int gate { get; private set; } = Settings.gate;


        /**
        *  container for average measurements
        */
        public double[] aggregatedData { get; private set; } = new double[8001];



    }
}
