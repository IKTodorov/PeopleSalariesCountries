using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSalariesCountries.Models;

namespace PeopleSalariesCountries.Database
{
    interface IDatabaseService
    {
        public List<Country> GetCountries();
        public Country GetCountry(int id);
        public void AddCountry(Country country);
    }
}
