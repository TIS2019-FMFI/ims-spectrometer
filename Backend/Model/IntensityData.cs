using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model{
    class IntensityData {

        /**
         * path where a corresponding csv file is located for this AggregatedData
         */
        public string path { get; set; } = "";


        /**
         * will contains data which will be display in Intensity Graph
         */
        public List<AggregatedData> intensityData { get; set; } = new List<AggregatedData>();


    }
}
