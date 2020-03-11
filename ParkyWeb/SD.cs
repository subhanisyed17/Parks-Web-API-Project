using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string apiBasePath = "https://localhost:44328/";
        public static string NationalParksAPI = apiBasePath + "api/v1/nationalparks";
        public static string TrailsAPI = apiBasePath + "api/v1/trails"; 
    }
}
