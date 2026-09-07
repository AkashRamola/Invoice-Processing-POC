using DAL.Database;
using DTO.Data;
using Services.Login.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login.Implementation
{
    public class LoginClass : ILoginInterface
    {
        private readonly InvoiceProcessingDbContext _context;
        public LoginClass(InvoiceProcessingDbContext context)
        {
            _context=context;
        }
        public CustomerInformationDTO GetUserLoginData(string username, string password)
        {
            CustomerInformationDTO LoginData = new CustomerInformationDTO();
            var userData=_context.CustomerInformations.Where(x=>x.UserName== username && x.Password== password).FirstOrDefault();
            if (userData != null)
            {
                LoginData.UserName=userData.UserName;
                LoginData.CustomerId=userData.CustomerId;
                LoginData.RoleId=userData.RoleId;
                LoginData.Createdon = userData.Createdon;
                LoginData.Name = userData.Name;
                LoginData.Password = userData.Password;
                return LoginData;
            }
            return null;
        }
    }
}
