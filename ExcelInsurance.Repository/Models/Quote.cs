using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelInsurance.Repository.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public string InsurerName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Pin { get; set; }
        public string AgentName { get; set; }
        public string AddressProofType { get; set; }
        public string BankAccountNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
    }
}
