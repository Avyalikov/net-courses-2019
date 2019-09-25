using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimateDBContext;
using ClimateDBContext.DAL;
using Microsoft.OData.Client;
using ClimateDBContext.Models;
using ClimateApp.ClimateDBContext.Models;

namespace ClimateApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example of using Service with ODataClient 
            string serviceUri = "http://localhost:57507/odata";
            var container = new Default.Container(new Uri(serviceUri));
            var countryService = new CountryService();
            var cityService = new CityService();
            //Create
            var country = new ClimateDBContext.Models.Country()
            {
                CountryName = "Finland",
                Region = WorldRegions.Eurasia

            };
            countryService.AddCountry(container, country);
            //Get/Read
            var countries = countryService.GetCountries(container);
            //Update
            countryService.UpdateCountry(container, 2, "Republic of China");
            //Delete
            countryService.DeleteCountry(container, 3);

        }
    }
}
