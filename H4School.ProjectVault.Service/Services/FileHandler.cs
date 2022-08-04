using H4School.ProjectVault.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H4School.ProjectVault.Service.Services
{
    public class FileHandler
    {
        public static void SavePasswordInFile(PasswordDTO password)
        {
            if (File.Exists(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords" + password.FileName + ".txt") == false)
            {
                File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords" + password.FileName + ".txt",
                    password.FileName + Environment.NewLine
                    + Convert.ToBase64String(password.HashedPassword) + Environment.NewLine
                    + Convert.ToBase64String(password.Salt) + Environment.NewLine);
            }
        }

        public static void EncryptFile()
        {

        }
    }
}
