// <copyright file="City.cs" company="SKorol">
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
    /// City description
    /// </summary>
    public class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<CityClimateInfo> Climate { get; set; }
        public virtual Country Country { get; set; }
    }
}
