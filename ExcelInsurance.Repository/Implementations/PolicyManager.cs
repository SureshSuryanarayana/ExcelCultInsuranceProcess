using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SQLite;

namespace ExcelInsurance.Repository.Implementations
{
    public class PolicyManager : IPolicyManager
    {
        IDbConnection dbConnection;

        public bool AddFile(byte[] fileBytes, string fileName , int policyId)
        {
            string query = @"insert into tblDocuments (Name, Data, PolicyId) values (@fileName, @fileBytes, @policyId)";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString)) {
                int result = dbConnection.Execute(query, new { fileName = fileName, fileBytes = fileBytes, policyId = policyId });
                return result > 0 ? true : false;
            }
        }

        public int AddPolicy(Policy policy)
        {
            string query = @"insert into tblPolicy
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
                                DocumentPath,
                                Type,
                                Gender) values 
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
                                @DocumentPath,
                                @Type,
                                @Gender);select last_insert_rowid()";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                int result = dbConnection.Query<int>(query, policy).Single();
                return result;
            }
        }

        public void DeleteFile(int policyId)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                int result = dbConnection.Execute("delete from tblDocuments where PolicyId=@Id", new { Id = policyId });
            }
        }

        public byte[] GetFile(int policyId)
        {
            byte[] bytes = new byte[1];
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString)) {
                var record = dbConnection.QueryFirst("Select Data from tblDocuments where PolicyId = @id", new { id = policyId });
                bytes = (byte[])record.Data;
            }
            return bytes;
        }

        public List<Policy> GetPolicies(string filter)
        {
            string query = filter == "ALL" ? "select * from tblPolicy" : "select * from tblPolicy where Type = @Type";
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                var data = dbConnection.Query<Policy>(query, new { Type=filter }).ToList();
                return data;
            }
        }

        public Policy GetPolicy(int policyId)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                return dbConnection.Query<Policy>("select * from tblPolicy where Id = @id", new { id = policyId }).SingleOrDefault();
            }
        }

        public bool RemovePolicy(int policyId)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                int result = dbConnection.Execute("delete from tblPolicy where Id=@Id", new { Id = policyId });
                return result > 0 ? true : false;
            }
        }

        public bool UpdatePolicy(Policy policy)
        {
            using (dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                string query = @"update tblPolicy set
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
                                    DocumentPath = @DocumentPath,
                                    Gender = @Gender where Id=@Id";
                int result = dbConnection.Execute(query, policy);
                return result > 0 ? true : false;
            }
        }
    }
}
