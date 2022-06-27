using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//this class is used to hash and secure the password locally
//idea is sourced from: https://stackoverflow.com/questions/4181198/how-to-hash-a-password
namespace MarketLib.src.Security
{
    class UserSecurity
    {
        private ConcurrentDictionary<string, string> userRecords = new ConcurrentDictionary<string, string>();

        //will store the user info into the userrecords
        public void storeUser(string username, string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            userRecords[username] = savedPasswordHash;
        }

        //pre-condition: username already exists.
        public void verifyUser(string username, string password)
        {
            if (!userRecords.ContainsKey(username))
                return;
            /* Fetch the stored value */
            string savedPasswordHash = userRecords[username];
            /*      Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /*      Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /*      Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /*      Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new Exception("incorrect data");
        }
    }
}
