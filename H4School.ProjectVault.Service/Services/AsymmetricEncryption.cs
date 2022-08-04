using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace H4School.ProjectVault.Service.Services
{
    public class AsymmetricEncryption
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using RSACryptoServiceProvider rsa = new(2048);

            rsa.PersistKeyInCsp = false;
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);
        }

        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            using RSACryptoServiceProvider rsa = new(2048);

            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_publicKey);

            byte[] cipherBytes = rsa.Encrypt(dataToEncrypt, false);
            return cipherBytes;
        }

        public byte[] DecryptData(byte[] dataToEncrypt)
        {
            using RSACryptoServiceProvider rsa = new(2048);

            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_privateKey);

            byte[] plain = rsa.Decrypt(dataToEncrypt, false);
            return plain;
        }
    }
}
