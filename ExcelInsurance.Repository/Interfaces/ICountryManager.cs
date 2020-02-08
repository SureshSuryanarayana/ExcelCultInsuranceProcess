using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelInsurance.Repository.Interfaces
{
    public interface ICountryManager
    {
        List<Country> GetCountries();

    }
}
