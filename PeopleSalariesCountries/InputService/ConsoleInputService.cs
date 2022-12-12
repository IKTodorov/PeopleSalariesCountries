using Microsoft.Extensions.Options;
using PeopleSalariesCountries.CalculationService;
using PeopleSalariesCountries.Database;
using PeopleSalariesCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSalariesCountries.InputService
{
    class ConsoleInputService
    {
        private static readonly ConsoleInputService instance = new ConsoleInputService(SQLiteService.Instanse, NetSalaryService.Instance);
        private readonly IDatabaseService databaseService;
        private readonly NetSalaryService netSalaryService;
        private ConsoleInputService(IDatabaseService databaseService, NetSalaryService netSalaryService)
        { this.databaseService = databaseService; this.netSalaryService = netSalaryService; }
        public static ConsoleInputService Instance { get { return instance; } }
        public void OpenMenu()
        {
            int option = 0;
            Console.WriteLine("Welcome to the main menu!");
            while (option != 4)
            {
                option = GetOptionFromMenu();
                Console.Clear();
                switch (option)
                {
                    case 1:
                        PrintCountriesOption();
                        ClearConsole();
                        break;
                    case 2:
                        CalculateNetSalaryOption();
                        ClearConsole();
                        break;
                    case 3:
                        AddCountryOption();
                        ClearConsole();
                        break;
                }

            }
        }
        private void ClearConsole()
        {
            Console.WriteLine("Press any key to go back to the menu");
            Console.ReadLine();
            Console.Clear();
        }
        private void PrintCountriesOption()
        {
            List<Country> countries = databaseService.GetCountries();
            foreach (Country country in countries)
            {
                Console.WriteLine(country.Name);
            }
        }
        private void CalculateNetSalaryOption()
        {
            List<Country> countries_options = databaseService.GetCountries();
            int country_choice;
            Console.WriteLine("Please select a country");
            foreach (var country in countries_options.Select((value, index) => new { value, index }))
            {
                Console.WriteLine(country.index + 1 + " " + country.value.Name);
            }
            while (!int.TryParse(Console.ReadLine(), out country_choice) || country_choice < 0 || country_choice > countries_options.Count)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            country_choice--;
            double gross_salary = GetGrossSalary();
            double net_salary = netSalaryService.Calculate(countries_options[country_choice], gross_salary);
            Console.WriteLine(net_salary);
        }
        private void AddCountryOption()
        {
            Country new_country = GetCountry();
            databaseService.AddCountry(new_country);
        }
        private int GetOptionFromMenu()
        {
            int option;
            Console.WriteLine("1. See countries \n2. Calculate Net Salary\n3. Add Country\n4. Exit");
            while (!int.TryParse(Console.ReadLine(), out option) || option < 0 || option > 4)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return option;
        }
        private Country GetCountry()
        {
            string name = GetCountryName();
            string currency = GetCurrency();
            double income_tax = GetIncomeTax();
            double social_contribution = GetSocialContribution();
            double min_amount_for_taxation = GetMinAmountForTaxation();
            double max_amount_for_social_contribution = GetMaxAmountForSocialContribution();
            Country country = new Country(name, currency, income_tax, social_contribution,
                min_amount_for_taxation, max_amount_for_social_contribution);
            return country;
        }
        private double GetGrossSalary()
        {
            double gross_salary = 0;
            Console.WriteLine("Enter salary:");

            while (!double.TryParse(Console.ReadLine(), out gross_salary) || gross_salary < 0)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return gross_salary;
        }
        private string GetCountryName()
        {
            string name;
            Console.WriteLine("Enter country name: ");
            name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter a correct value!");
                name = Console.ReadLine();
            }
            return name;
        }
        private string GetCurrency()
        {
            string currency;
            Console.WriteLine("Enter currency: ");
            currency = Console.ReadLine();
            while (string.IsNullOrEmpty(currency))
            {
                Console.WriteLine("Please enter a correct value!");
                currency = Console.ReadLine();
            }
            return currency;
        }
        private double GetIncomeTax()
        {
            double income_tax = 0;
            Console.WriteLine("Enter income tax in %:");

            while (!double.TryParse(Console.ReadLine(), out income_tax) || income_tax < 0 || income_tax > 100)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return income_tax / 100;
        }
        private double GetSocialContribution()
        {
            double social_contribution = 0;
            Console.WriteLine("Enter social contribution in %:");

            while (!double.TryParse(Console.ReadLine(), out social_contribution) || social_contribution < 0 || social_contribution > 100)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return social_contribution / 100;
        }
        private double GetMinAmountForTaxation()
        {
            double min_amount_for_taxation = 0;
            Console.WriteLine("Enter minimal amount for taxation in :");

            while (!double.TryParse(Console.ReadLine(), out min_amount_for_taxation) || min_amount_for_taxation < 0)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return min_amount_for_taxation;
        }
        private double GetMaxAmountForSocialContribution()
        {
            double max_amount_for_social_contribution = 0;
            Console.WriteLine("Enter max amount for social contribution in :");

            while (!double.TryParse(Console.ReadLine(), out max_amount_for_social_contribution) || max_amount_for_social_contribution < 0)
            {
                Console.WriteLine("Please enter a correct value!");
            }
            return max_amount_for_social_contribution;
        }

    }
}
