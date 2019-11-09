using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Arduin.Backend.Model{

    /**
     * One measurement , lasting maximum 20 miliseconds
     */
    public class Measurement{

        /**
         * container for measured data by arduino, maximum 8000 points + 1 for save
         */
        public double[] measurement { get; set; } = new double[8001];
    }



}
