// <copyright file="CountryController.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace WebAppWithOData.ODataControllers
{
    using Microsoft.AspNet.OData;
    using ClimateDBContext.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ClimateDBContext.DAL;

    /// <summary>
    /// CountryController description
    /// </summary>
    public class CountriesController : GenericController<Country>
    {

        [EnableQuery]
        public IQueryable<City> GetCities([FromODataUri] int key)
        {
            var country = dbContext.Countries.Find(key);
            var cities = country.Cities.AsQueryable();
            return cities;
        }

        public override bool IdCheck(int id, Country country)
        {
            return id == country.CountryID;
        }
    }
}
