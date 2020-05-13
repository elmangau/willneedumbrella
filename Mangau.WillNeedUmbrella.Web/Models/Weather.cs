using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Web.Models
{
    public class WeatherWeather
    {
        public int Id { get; set; }

        public string Main { get; set; }
    }

    public class WeatherDetail
    {
        public List<WeatherWeather> Weather { get; set; }

        public DateTime Dt_Txt { get; set; }
    }

    public class Weather
    {
        public string Cod { get; set; }

        public List<WeatherDetail> List { get; set; }
    }
}
