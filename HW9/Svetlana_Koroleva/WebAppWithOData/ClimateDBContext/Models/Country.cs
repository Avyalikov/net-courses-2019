// <copyright file="Country.cs" company="SKorol">
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
    /// Country description
    /// </summary>
    public class Country
    {
        public enum WorldRegions
        {
            Eurasia,
            Africa,
            NorthAmerica,
            SouthAmerica,
            Australia
        }

        public int CountryID { get; set; }
        public WorldRegions Region { get; set; }
        public string CountryName { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        
    }
}
