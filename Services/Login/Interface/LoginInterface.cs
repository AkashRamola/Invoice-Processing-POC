using DTO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login.Interface
{
    public interface ILoginInterface
    {
        public CustomerInformationDTO GetUserLoginData(string username, string password);
    }
}
