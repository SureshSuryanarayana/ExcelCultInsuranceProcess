using ExcelInsurance.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcelInsurance.Repository.Interfaces
{
    public interface IQuoteManager
    {
        List<Quote> GetQuotes();
        Quote GetQuote(int quoteId);
        bool AddQuote(Quote quote);
        bool RemoveQuote(int quoteId);
        bool UpdateQuote(Quote quote);
    }
}
