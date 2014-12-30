using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geodesy101
{
    public class LatLonE  
    {

        double _lat, _lon;
        public double lat 
        {
            get { return _lat; }
            set {  _lat = value; } 
        }
        public double lon 
        {
            get { return _lon; }
            set { _lon = value; }
        }
        public LatLonE() { }

        public LatLonE(double lat, double lon)
        { 
        
        
        }

    }
}
