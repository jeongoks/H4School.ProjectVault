using H4School.ProjectVault.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            #region Symmetric
            //SymmetricEncryption _aes = new();
            //EncryptedPacketDTO encryptedPacket = new EncryptedPacketDTO();
            //encryptedPacket.EncryptedSessionKey = _aes.GenerateRandomNumber(32);
            //encryptedPacket.Iv = _aes.GenerateRandomNumber(16);

            // Choosing which filetext to read from by using our fileName variable.
            //var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt");
            // Encrypting the above variable using Symmetric encryption.
            //string encrypted = Convert.ToBase64String(_aes.Encrypt(Encoding.UTF8.GetBytes(fileText), encryptedPacket.EncryptedSessionKey, encryptedPacket.Iv));
            #endregion

            X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

            // Choosing which filetext to read from by using our fileName variable.
            var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt");
            if (fileText != "")
            {
                string encrypted = Certificate.Encrypt(myCertificate, fileText);
                SavingEncryptedFiles(encrypted, fileName);
            }        
        }

        public static void DecryptData(string fileName)
        {
            X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

            var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\EncryptedPasswords\" + fileName + ".txt");
            if (fileText != "")
            {
                string decrypted = Certificate.Decrypt(myCertificate, fileText);
                SavingDecryptedFiles(decrypted, fileName);
            }
        }

        public static void SavingEncryptedFiles(string encryptedData, string fileName)
        {
            if (File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt") != "")
            {
                File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\EncryptedPasswords\" + fileName + ".txt", encryptedData);
            }
        }

        public static void SavingDecryptedFiles(string decryptedData, string fileName)
        {
            File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\DecryptedPasswords" + fileName + ".txt", decryptedData);
        }
    }
}
