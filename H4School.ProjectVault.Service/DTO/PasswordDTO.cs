using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H4School.ProjectVault.Service.DTO
{
    public class PasswordDTO
    {
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
