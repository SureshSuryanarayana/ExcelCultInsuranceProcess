using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Data.SQLite;

namespace ExcelInsurance.Repository.Implementations
{
    public class QuoteManager : IQuoteManager
    {
        IDbConnection dbConnection;
        public int AddQuote(Quote quote)
        {
            string query = @"insert into tblQuote
                                (StartDate,
                                EndDate,
                                Amount,
                                InsurerFirstName,
                                InsurerLastName,
                                InsurerMiddleName,
                                Address,
                                State,
                                Country,
                                Pin,
                                AgentName,
                                AddressProofType,
                                BankAccountNumber,
                                Email,
                                Phone,
                                Nominee,
                                Relation,
                                Type,
                                Gender
                                ) values 
                                (@StartDate,
                                @EndDate,
                                @Amount,
                                @InsurerFirstName,
                                @InsurerLastName,
                                @InsurerMiddleName,
                                @Address,
                                @State,
                                @Country,
                                @Pin,
                                @AgentName,
                                @AddressProofType,
                                @BankAccountNumber,
                                @Email,
                                @Phone,
                                @Nominee,
                                @Relation,
                                @Type,
                                @Gender
                                );select last_insert_rowid()";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                int result = dbConnection.Query<int>(query, quote).Single();
                return result;
            }
        }

        public Quote GetQuote(int quoteId)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                return dbConnection.Query<Quote>("select * from tblQuote where Id = @id", new { id = quoteId }).SingleOrDefault();
            }
        }

        public List<Quote> GetQuotes(string filter)
        {
            string query = filter == "ALL" ? "select * from tblQuote" : "select * from tblQuote where Type=@Type";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                return dbConnection.Query<Quote>(query,new { Type = filter }).ToList();
            }
        }

        public bool RemoveQuote(int quoteId)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                int result = dbConnection.Execute("delete from tblQuote where Id=@Id", new { Id = quoteId });
                return result > 0 ? true : false;
            }
        }

        public bool UpdateQuote(Quote quote)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                string query = @"update tblQuote set
                                    StartDate = @StartDate, 
                                    EndDate = @EndDate,
                                    Amount = @Amount,
                                    InsurerFirstName = @InsurerFirstName, 
                                    InsurerLastName = @InsurerLastName, 
                                    InsurerMiddleName = @InsurerMiddleName,
                                    Address = @Address,
                                    State = @State,
                                    Country = @Country,
                                    Pin = @Pin,
                                    AgentName = @AgentName,
                                    AddressProofType = @AddressProofType,
                                    BankAccountNumber = @BankAccountNumber,
                                    Email = @Email,
                                    Phone = @Phone,
                                    Nominee = @Nominee,
                                    Relation = @Relation,
                                    Type = @Type,
                                    Gender = @Gender
                                    where Id=@Id";
                int result = dbConnection.Execute(query, quote);
                return result > 0 ? true : false;
            }
        }
    }
}
