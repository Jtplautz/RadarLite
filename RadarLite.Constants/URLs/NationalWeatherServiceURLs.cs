using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarLite.Constants.URLs;
//https://www.weather.gov/documentation/services-web-api
public static class NationalWeatherServiceURLs {
    public const string BASE_URI = "https://api.weather.gov/";
    public const string GEOPOSITION = "points/";
    public const string STATIONS_BY_STATE = "stations?state=";
    public const string LOCATION_API = "locations/v1/";
    public const string FORECAST_API = "forecasts/v1/";
}

