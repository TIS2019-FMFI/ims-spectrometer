using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduin.Backend.Model{

    // use as Mobility.X
    public sealed class Mobility {

        public static double L { get; set; } = 13.05;  // (dlzku trubice-L(cm)

        public static double p { get; set; } = 700.0;  // tlak plynu - p- (pa)

        public static double T { get; set; } = 293.0;  // teplotu plynu T(K)

        public static double U { get; set; } = 7.000;  // napatie na driftovej trubici U (kV)) 


    }
}
