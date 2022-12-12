using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSalariesCountries.Models
{
    class Country
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public double Income_tax { get; set; }
        public double Social_contribution { get; set; }
        public double Min_amount_for_taxation { get; set; }
        public double Max_amount_for_social_contribution { get; set; }

        public Country(string name, string currency, double income_tax, double social_contributions,
            double min_amount_for_taxation, double max_amount_for_social_contribution)
        {
            Name = name;
            Currency = currency;
            Income_tax = income_tax;
            Social_contribution = social_contributions;
            Min_amount_for_taxation = min_amount_for_taxation;
            Max_amount_for_social_contribution = max_amount_for_social_contribution;
        }

    }

}
