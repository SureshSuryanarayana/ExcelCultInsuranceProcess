using ExcelInsurance.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcelInsurance.Repository.Interfaces
{
    public interface IPolicyManager
    {
        List<Policy> GetPolicies(string filter);
        Policy GetPolicy(int policyId);
        int AddPolicy(Policy policy);
        bool RemovePolicy(int policyId);
        bool UpdatePolicy(Policy policy);

        bool AddFile(byte[] fileBytes, string fileName, int policyId);
        byte[] GetFile(int policyId);

        void DeleteFile(int policyId);
    }
}
