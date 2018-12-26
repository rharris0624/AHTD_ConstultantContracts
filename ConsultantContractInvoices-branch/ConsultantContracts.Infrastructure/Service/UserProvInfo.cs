using ArDOT_UserProv.Client.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantContracts.Infrastructure.Service
{
    public class UserProvInfo
    {
        public Security[] GetUserSecurity(string userName)
        {
            Security[] response = null;

            try
            {
                var userProvHelper = new UserProvHelper();

                response = userProvHelper.GetApplicationSecuritiesForUser(userName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}
