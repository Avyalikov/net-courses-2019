// <copyright file="ClimateInfoController.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace WebAppWithOData.ODataControllers
{
    using ClimateDBContext.DAL;
    using ClimateDBContext.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ClimateInfoController description
    /// </summary>
    public class ClimateInfoController : GenericController<CityClimateInfo>
    {
        public override bool IdCheck(int id, CityClimateInfo entity)
        {
            return id == entity.CityClimateInfoID;
        }
    }
}
