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
                    password.FileName + Environment.NewLine
                    + Convert.ToBase64String(password.HashedPassword) + Environment.NewLine
                    + Convert.ToBase64String(password.Salt) + Environment.NewLine);
            }
        }

        /// <summary>
        /// This method shows all hashed passwords, by using decryption on the encrypted file.
        /// </summary>
        public static void ShowAllHashedPassword()
        {
            List<PasswordDTO> passwordDTOs = new List<PasswordDTO>();
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\");
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Extension == ".txt")
                {
                    string fileData = File.ReadAllText(fileInfo.FullName);
                    X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

                    string decrypted = Certificate.Decrypt(myCertificate, fileData);
                    string[] passwords = decrypted.Split(Environment.NewLine);
                    passwordDTOs.Add(new PasswordDTO
                    {
                        FileName = passwords[0],
                        HashedPassword = Convert.FromBase64String(passwords[1]),
                        Salt = Convert.FromBase64String(passwords[2]),
                    });
                }
            }            

            foreach (var item in passwordDTOs)
            {
                Console.WriteLine($"File name : {item.FileName}");
                Console.WriteLine($"Hashed Password : {Convert.ToBase64String(item.HashedPassword)}");
                Console.WriteLine($"Salt : {Convert.ToBase64String(item.Salt)}");
                Console.WriteLine();
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

        /// <summary>
        /// This method is used to override the save file of the hashed password.
        /// </summary>
        /// <param name="encryptedData">The encrypted data from <see cref="EncryptFile(string)"/></param>
        /// <param name="fileName">The file which is needed to be encrypted.</param>
        public static void SavingEncryptedFiles(string encryptedData, string fileName)
        {
            if (File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt") != "")
            {
                File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt", encryptedData);
            }
        }

        //public static void DecryptData(string fileName)
        //{
        //    X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

        //    var fileText = File.ReadAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt");
        //    if (fileText != "")
        //    {
        //        string decrypted = Certificate.Decrypt(myCertificate, fileText);
        //        SavingDecryptedFiles(decrypted, fileName);
        //    }
        //}

        /// <summary>
        /// This method is used to decrypt the encrypted file, so a user can view their hashed passwords.
        /// </summary>
        /// <param name="decryptedData"></param>
        /// <param name="fileName"></param>
        //public static void SavingDecryptedFiles(string decryptedData, string fileName)
        //{
        //    File.WriteAllText(@"C:\Users\Jasmin\source\repos\H4School.ProjectVault\SecuredPasswords\" + fileName + ".txt", decryptedData);
        //}
    }
}
