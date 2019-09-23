// <copyright file="DBInitializer.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ClimateDBContext.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClimateDBContext.Models;

    /// <summary>
    /// DBInitializer description
    /// </summary>
    public class DBInitializer : CreateDatabaseIfNotExists<ClimateDbContext>
    {
        protected override void Seed(ClimateDbContext climateDbContext)
        {
            IEnumerable<Country> countries = new List<Country>()
            {
            new Country { CountryName = "Russia", Region = Country.WorldRegions.Eurasia },
            new Country { CountryName = "China", Region = Country.WorldRegions.Eurasia },
            new Country { CountryName = "Bahamas", Region = Country.WorldRegions.NorthAmerica }
            };
            climateDbContext.Countries.AddRange(countries);
            climateDbContext.SaveChanges();

            IEnumerable<City> cities = new List<City>()
            {
            new City {CountryID=1, CityName="Saint-Petersburg"},
            new City {CountryID=2, CityName="Beijing"},
            new City {CountryID=3, CityName="Nassau"},
            };
            climateDbContext.Cities.AddRange(cities);
            climateDbContext.SaveChanges();

            IEnumerable<CityClimateInfo> citiesClimate = new List<CityClimateInfo>()
            {
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.january, AvgTemperature=-5.6,  WaterTemperature=-1, SunnyDays=2   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.february, AvgTemperature=-5.1,  WaterTemperature=-1.3, SunnyDays=2   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.march, AvgTemperature=-1.2,  WaterTemperature=-1, SunnyDays=3   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.april, AvgTemperature=5.5,  WaterTemperature=1.8, SunnyDays=7   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.may, AvgTemperature=11.5,  WaterTemperature=8.4, SunnyDays=13   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.june, AvgTemperature=16,  WaterTemperature=13.1, SunnyDays=14   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.july, AvgTemperature=19.5,  WaterTemperature=16, SunnyDays=16   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.august, AvgTemperature=17.4,  WaterTemperature=17.3, SunnyDays=14   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.september, AvgTemperature=12.7,  WaterTemperature=13.9, SunnyDays=7   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.october, AvgTemperature=6.3,  WaterTemperature=7.1, SunnyDays=6   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.november, AvgTemperature=1.4,  WaterTemperature=3.3, SunnyDays=3   },
            new CityClimateInfo { CityID=1, Month=CityClimateInfo.Months.december, AvgTemperature=-2.3,  WaterTemperature=0.8, SunnyDays=2   },

            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.january, AvgTemperature=-3.1,  WaterTemperature=4, SunnyDays=25   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.february, AvgTemperature=0.3,  WaterTemperature=3, SunnyDays=21   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.march, AvgTemperature=6.7,  WaterTemperature=4, SunnyDays=24   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.april, AvgTemperature=14.8,  WaterTemperature=7, SunnyDays=22   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.may, AvgTemperature=20.8,  WaterTemperature=12, SunnyDays=23   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.june, AvgTemperature=24.9,  WaterTemperature=18, SunnyDays=23   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.july, AvgTemperature=26.7,  WaterTemperature=23, SunnyDays=20   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.august, AvgTemperature=25.5,  WaterTemperature=25, SunnyDays=22   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.september, AvgTemperature=20.8,  WaterTemperature=23, SunnyDays=23   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.october, AvgTemperature=13.7,  WaterTemperature=19, SunnyDays=25   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.november, AvgTemperature=5,  WaterTemperature=14, SunnyDays=22   },
            new CityClimateInfo { CityID=2, Month=CityClimateInfo.Months.december, AvgTemperature=0.9,  WaterTemperature=8, SunnyDays=24   },

            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.january, AvgTemperature=22.9,  WaterTemperature=25, SunnyDays=9   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.february, AvgTemperature=23.3,  WaterTemperature=24, SunnyDays=10   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.march, AvgTemperature=23.3,  WaterTemperature=24, SunnyDays=10   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.april, AvgTemperature=25.6,  WaterTemperature=25, SunnyDays=11   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.may, AvgTemperature=26.1,  WaterTemperature=26, SunnyDays=11   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.june, AvgTemperature=28,  WaterTemperature=28, SunnyDays=10   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.july, AvgTemperature=29.3,  WaterTemperature=29, SunnyDays=12   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.august, AvgTemperature=29.2,  WaterTemperature=29, SunnyDays=11   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.september, AvgTemperature=28.7,  WaterTemperature=29, SunnyDays=11   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.october, AvgTemperature=27.3,  WaterTemperature=28, SunnyDays=9   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.november, AvgTemperature=25.3,  WaterTemperature=27, SunnyDays=11   },
            new CityClimateInfo { CityID=3, Month=CityClimateInfo.Months.december, AvgTemperature=24.2,  WaterTemperature=26, SunnyDays=9   }

            };
            climateDbContext.ClimateInfos.AddRange(citiesClimate);
            climateDbContext.SaveChanges();
        }
    }
}
