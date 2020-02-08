using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;
using System.Configuration;

namespace ExcelInsurance.Repository.Implementations
{
    public class CountryManager : ICountryManager
    {
        private IDbConnection dbConnection;
        public List<Country> GetCountries()
        {
            var query = "Select * from tblCountry";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString)) {
                return dbConnection.Query<Country>(query).ToList();
            }
        }
    }
}
