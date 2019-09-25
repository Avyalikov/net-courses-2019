// <copyright file="CitiesController.cs" company="SKorol">
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
    using ClimateDBContext.DAL;

    /// <summary>
    /// CitiesController description
    /// </summary>
    public class CitiesController : GenericController<City>
    {

        [EnableQuery]
        public IQueryable<CityClimateInfo> GetClimate([FromODataUri] int key)
        {
            var city = dbContext.Cities.Find(key);
            var climateInfo = city.Climate;
            return climateInfo.AsQueryable();
        }

        public override bool IdCheck(int id, City entity)
        {
            return id == entity.CityID;
        }
    }
}
