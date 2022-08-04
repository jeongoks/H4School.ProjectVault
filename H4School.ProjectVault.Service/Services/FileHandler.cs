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
            if (File.Exists(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + password.FileName + ".txt") == false)
            {
                File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + password.FileName + ".txt",
                    Convert.ToBase64String(password.HashedPassword) + Environment.NewLine
                    + Convert.ToBase64String(password.Salt) + Environment.NewLine);
            }
        }

        public static void EncryptFile(string fileName)
        {
            SymmetricEncryption _aes = new();
            EncryptedPacketDTO encryptedPacket = new EncryptedPacketDTO();
            encryptedPacket.EncryptedSessionKey = _aes.GenerateRandomNumber(32);
            encryptedPacket.Iv = _aes.GenerateRandomNumber(16);

            // Choosing which filetext to read from by using our fileName variable.
            var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt");

            // Encrypting the above variable using Symmetric encryption.
            string encrypted = Convert.ToBase64String(_aes.Encrypt(Encoding.UTF8.GetBytes(fileText), encryptedPacket.EncryptedSessionKey, encryptedPacket.Iv));

            SavingEncryptedFiles(encrypted, fileName);            
        }

        public static void DecryptData(string fileName)
        {
            SymmetricEncryption _aes = new();
            EncryptedPacketDTO encryptedPacketDTO = new EncryptedPacketDTO();  

            var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\EncryptedPasswords\" + fileName + ".txt");
            //string decrypted = Convert.ToBase64String(_aes.Decrypt(Encoding.UTF8.GetBytes(fileText), )
        }

        public static void SavingEncryptedFiles(string encryptedData, string fileName)
        {
            if (File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt") != "")
            {
                File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\EncryptedPasswords\" + fileName + ".txt", encryptedData);
            }
        }
    }
}
