using System.Security.Cryptography;

namespace BikeSparesInventorySystem.Data.Utils
{
    internal static class Hasher
    {
        const char SEGMENT_DELIMITER = ':';
        const int SALT_SIZE = 16;
        const int ITERATIONS  = 100_000;
        const int KEY_SIZE = 32;
        static readonly HashAlgorithmName ALGORITHM = HashAlgorithmName.SHA256;

        public static string HashSecret(string input, HashAlgorithmName? hashAlgorithm = null, int saltSize = SALT_SIZE, int iterations = ITERATIONS, int keySize = KEY_SIZE)
        {

            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);            
            var algorithm = hashAlgorithm ?? ALGORITHM;
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

            return string.Join(
                SEGMENT_DELIMITER,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(SEGMENT_DELIMITER);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        /*        public static string GetAppDirectoryPath()
                {
                    return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "Islington-Todo"
                    );
                }

                public static string GetAppUsersFilePath()
                {
                    return Path.Combine(GetAppDirectoryPath(), "users.json");
                }

                public static string GetTodosFilePath(Guid userId)
                {
                    return Path.Combine(GetAppDirectoryPath(), userId.ToString() + "_todos.json");
                }*/

    }
}
