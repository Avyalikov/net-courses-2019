// <copyright file="CityService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ClimateApp
{
    using ClimateDBContext.Models;
    using Microsoft.OData.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// CityService description
    /// </summary>
    public class CityService
    {
       public List<City> GetCities(Default.Container container)
        {
            return container.Cities.ToList();
        }

        public void AddCity(Default.Container container, City city)
        {
            if (container.Cities.Where(c=>c.CityName==city.CityName&&c.CountryID==city.CountryID).Count()!=0)
            {
                return;
            }
            container.AddToCities(city);
            var serviceResponse = container.SaveChanges();
        }

        public void UpdateCity(Default.Container container, int id, string cityName)
        {
            var city = container.Cities.Where(c => c.CityID == id).SingleOrDefault();
            if (city != null)
            {
                city.CityName = cityName;
                container.UpdateObject(city);
                container.SaveChanges();
            }
        }

        public void DeleteCity(Default.Container container, int id)
        {
            try
            {
                var city = container.Cities.Where(c => c.CityID == id).Single();
                if (city != null)
                {
                    container.DeleteObject(city);
                    container.SaveChanges();
                }
            }
            catch (DataServiceQueryException)
            {
                return;
            }
        }

        public void FilterCitiesByNames(Default.Container container, string name)
        {
            var cities =
                from c in container.Cities
                where c.CityName == name
                select c;
        }
    }
}
