using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Data
{
    public class CustomerInformationDTO
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime Createdon { get; set; }

        public int? RoleId { get; set; }
    }
}
