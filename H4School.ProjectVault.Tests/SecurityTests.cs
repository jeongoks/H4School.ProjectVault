using H4School.ProjectVault.Service.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace H4School.ProjectVault.Tests
{
    public class SecurityTests
    {
        [Fact]
        public void Can_Combine_Password_And_Salt()
        {
            // ARRANGE: 
            const string password = "P@ssw0rd";
            byte[] salt = HashingPassword.GenerateSalt();

            // ACT:           
            byte[] hashedPassword = HashingPassword.HashingPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);

            // ASSERT:
            Assert.NotNull(hashedPassword);
        }

        [Fact]
        public void Can_Encrypt_A_Hashed_String()
        {
            // ARRANGE:
            const string password = "P@ssw0rd";
            byte[] salt = HashingPassword.GenerateSalt();
            byte[] hashedPassword = HashingPassword.HashingPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");

            // ACT:
            string encrypted = Certificate.Encrypt(myCertificate, Convert.ToBase64String(hashedPassword));

            // ASSERT:
            Assert.NotNull(encrypted);
        }

        [Fact]
        public void Can_Decrypt_An_Encrypted_String()
        {
            // ARRANGE:
            const string password = "P@ssw0rd";
            byte[] salt = HashingPassword.GenerateSalt();
            byte[] hashedPassword = HashingPassword.HashingPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            X509Certificate2 myCertificate = Certificate.LoadCertificate(StoreLocation.CurrentUser, "CN=CryptoCert");
            string encrypted = Certificate.Encrypt(myCertificate, Convert.ToBase64String(hashedPassword));

            // ACT:
            string decrypted = Certificate.Decrypt(myCertificate, encrypted);

            // ASSERT:
            Assert.Equal(decrypted, Convert.ToBase64String(hashedPassword));
        }
    }
}