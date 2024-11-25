using System.Collections.Generic;

namespace TravelApi.Models
{
    public class RouteResponse
    {
        public List<List<double>> Coordinates { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
    }
}
