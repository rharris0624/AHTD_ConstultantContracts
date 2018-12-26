using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultantContracts.Infrastructure.Service;

namespace ConsultantContracts.Infrastructure.Adapters.Service
{
    public class UserProvAdapter
    {
        IEnumerable<string> GetUserBudgets(string userName)
        {
            IEnumerable<string> response = null;
            try
            {
                var userProvInfo = new UserProvInfo();
                var securities = userProvInfo.GetUserSecurity(userName);
                response = from security in securities where security.Resource.ResourceName.Contains("Budget") select security.Resource.ResourceId;
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }
    }
}
