using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSalariesCountries.Models;

namespace PeopleSalariesCountries.CalculationService
{
    class NetSalaryService
    {
        private static NetSalaryService instance = new NetSalaryService();
        private NetSalaryService() { }
        public static NetSalaryService Instance { get { return instance; } }
        public double Calculate(Country country, double gross_salary)
        {
            double net_salary = gross_salary;
            if (gross_salary <= country.Min_amount_for_taxation)
            {
                return net_salary;
            }
            double amount_to_tax = gross_salary - country.Min_amount_for_taxation;
            net_salary -= amount_to_tax * country.Income_tax;

            if (amount_to_tax > country.Max_amount_for_social_contribution)
            {
                net_salary -= country.Max_amount_for_social_contribution * country.Social_contribution;
                return net_salary;
            }
            net_salary -= amount_to_tax * country.Social_contribution;
            return net_salary;
        }
    }
}
