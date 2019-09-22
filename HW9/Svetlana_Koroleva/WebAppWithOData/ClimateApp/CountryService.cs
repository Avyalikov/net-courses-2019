// <copyright file="CountryService.cs" company="SKorol">
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
    /// CountryService description
    /// </summary>
    public class CountryService
    {
       public List<Country> GetCountries(Default.Container container)
        {
            return container.Countries.ToList();
        }

        public void AddCountry(Default.Container container, Country country)
        {
            if (container.Countries.Where(c=>c.CountryName==country.CountryName).Count()!=0)
            {
                return;
            }
            container.AddToCountries(country);
            var serviceResponse = container.SaveChanges();
        }

        public void UpdateCountry(Default.Container container, int id, string countryName)
        {
            var country = container.Countries.Where(c => c.CountryID == id).SingleOrDefault();
            if (country != null)
            {
                country.CountryName = countryName;
                container.UpdateObject(country);
                container.SaveChanges();
            }
        }

        public void DeleteCountry(Default.Container container, int id)
        {
            try
            {
                var country = container.Countries.Where(c => c.CountryID == id).Single();
                if (country != null)
                {
                    container.DeleteObject(country);
                    container.SaveChanges();
                }
            }
            catch (DataServiceQueryException)
            {
                return;
            }
        }

        public void FilterCountriesByNames(Default.Container container, string name)
        {
            var countries =
                from c in container.Countries
                where c.CountryName == name
                select c;
        }
    }
}
