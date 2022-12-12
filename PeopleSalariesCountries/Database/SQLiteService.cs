using Microsoft.Data.Sqlite;
using PeopleSalariesCountries.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PeopleSalariesCountries.Database
{
    class SQLiteService : IDatabaseService
    {
        private static SQLiteService instanse = new SQLiteService();
        private SQLiteService() { }
        public static SQLiteService Instanse { get { return instanse; } }
        public void AddCountry(Country country)
        {
            using var connection = new SqliteConnection("Data Source=hello.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                @"
                    INSERT INTO country (name, currency, income_tax, social_contribution, min_amount, max_amount_sc)
                    VALUES ($name, $currency, $income_tax, $social_contribution, $min_amount, $max_amount_sc)
                ";
            command.Parameters.AddWithValue("$name", country.Name);
            command.Parameters.AddWithValue("$currency", country.Currency);
            command.Parameters.AddWithValue("$income_tax", country.Income_tax);
            command.Parameters.AddWithValue("$social_contribution", country.Social_contribution);
            command.Parameters.AddWithValue("$min_amount", country.Min_amount_for_taxation);
            command.Parameters.AddWithValue("$max_amount_sc", country.Max_amount_for_social_contribution);

            command.ExecuteNonQuery();
        }

        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using var connection = new SqliteConnection("Data Source=hello.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT * FROM country
            ";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(1);
                string currency = reader.GetString(2);
                double income_tax = reader.GetDouble(3);
                double social_contribution = reader.GetDouble(4);
                double min_income = reader.GetDouble(5);
                double max_for_social = reader.GetDouble(6);

                countries.Add(new Country(name, currency, income_tax, social_contribution, min_income, max_for_social));
            }
            return countries;
        }

        public Country GetCountry(int id)
        {
            string name = "";
            string currency = "";
            double income_tax = 0;
            double social_contribution = 0;
            double min_income = 0;
            double max_for_social = 0;

            using var connection = new SqliteConnection("Data Source=hello.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                    SELECT * FROM country WHERE id = $id
                ";
            command.Parameters.AddWithValue("$id", id);


            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(1);
                currency = reader.GetString(2);
                income_tax = reader.GetDouble(3);
                social_contribution = reader.GetDouble(4);
                min_income = reader.GetDouble(5);
                max_for_social = reader.GetDouble(6);
            }
            Country country = new Country(name, currency, income_tax, social_contribution, min_income, max_for_social);
            return country;

        }
    }
}
