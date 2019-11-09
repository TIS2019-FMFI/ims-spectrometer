using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model{
    public class IntensityData {

        /**
         * will contains data which will be display in Intensity Graph
         */
        public List<AggregatedData> intensityData { get; set; } = new List<AggregatedData>();

        /**
         * determines whether graph is generated from main graph in current time.
         * True - if this graph needs to be updated on UI
         * False - if it was displayed from CSV files
         */
        public bool ongoingMeasurement { get; set; } = false;


    }
}
