// <copyright file="CityClimateService.cs" company="SKorol">
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
    /// CityClimateService description
    /// </summary>
    public class CityClimateService
    {
       public List<CityClimateInfo> GetCities(Default.Container container)
        {
            return container.ClimateInfo.ToList();
        }

        public void AddCityClimate(Default.Container container, CityClimateInfo cityClimate)
        {
            if (container.ClimateInfo.Where(ci=>ci.CityID==cityClimate.CityID&&ci.Month==cityClimate.Month).Count()!=0)
            {
                return;
            }
            container.AddToClimateInfo(cityClimate);
            var serviceResponse = container.SaveChanges();
        }

        public void UpdateCityClimate(Default.Container container, int id, double awgTemp)
        {
            var CityClimate = container.ClimateInfo.Where(c => c.CityClimateInfoID== id).SingleOrDefault();
            if (CityClimate != null)
            {
                CityClimate.AvgTemperature = awgTemp;
                container.UpdateObject(CityClimate);
                container.SaveChanges();
            }
        }

        public void DeleteCityClimate(Default.Container container, int id)
        {
            try
            {
                var CityClimate = container.ClimateInfo.Where(c => c.CityClimateInfoID == id).Single();
                if (CityClimate != null)
                {
                    container.DeleteObject(CityClimate);
                    container.SaveChanges();
                }
            }
            catch (DataServiceQueryException)
            {
                return;
            }
        }

        public void FilterCityClimateByTemp(Default.Container container, double awgTemp)
        {
            var cityClimateInfo =
                from c in container.ClimateInfo
                where c.AvgTemperature== awgTemp
                select c;
        }
    }
}
