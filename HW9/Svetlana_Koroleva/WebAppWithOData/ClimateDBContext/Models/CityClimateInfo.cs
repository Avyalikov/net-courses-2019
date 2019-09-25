// <copyright file="CityClimateInfo.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ClimateDBContext.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// CityClimateInfo description
    /// </summary>
    public class CityClimateInfo
    {
        public enum Months
        {
            january,
            february,
            march,
            april,
            may,
            june,
            july,
            august,
            september,
            october,
            november,
            december
        }
        public int CityClimateInfoID { get; set; }
        public int CityID { get; set; }
        public Months Month { get; set; }
        public double AvgTemperature { get; set; }
        public double WaterTemperature { get; set; }
        public int SunnyDays { get; set; }
        public virtual City City { get; set; }
    }
}
