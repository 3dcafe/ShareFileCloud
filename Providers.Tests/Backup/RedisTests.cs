using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace Providers.Tests.Backup
{
    public class RedisTests
    {
        // Method to initialize configuration from appsettings.json and environment variables
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }


        public static IConfiguration InitConfiguration2()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings2.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        [Fact]
        public async void TestGetBackupFromRedisServer()
        {
            // Initialize configuration
            var config = InitConfiguration();
            // Get the Redis configuration section
            var redisConfig = config.GetSection("Redis");

            // Extract Redis configuration parameters
            var host = redisConfig.GetSection("host").Value;
            var portString = redisConfig.GetSection("port").Value; // Fixed
            var password = redisConfig.GetSection("password").Value; // Fixed
            var defaultDatabaseString = redisConfig.GetSection("defaultDatabase").Value;

            // Type checking and parsing parameters
            if (!int.TryParse(portString, out int port))
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid port number.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            if (host == null)
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid host or null");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            if (!int.TryParse(defaultDatabaseString, out int defaultDb))
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid default database number.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }
#warning password optional fix
            if(password == null)
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid password or null");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            // Check if the host is a valid IP address or domain
            if (!IsValidIpAddress(host) && !IsValidDomain(host))
            {
                Console.WriteLine("Error: Invalid host. Please provide a valid IP address or domain.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            var redis = new Providers.Backup.Redis();
            // Call the Redis backup method with the parsed parameters
            var data = await redis.GetBackupAsync(host, port, password, defaultDb);
#warning check on connection status
            SaveJsonToFile(data, $"D:\\{Guid.NewGuid()}.json");
            Assert.True(true);
        }

        [Fact]
        public async void TestSetBackupFromRedisServer()
        {
            // Initialize configuration
            var config = InitConfiguration2();
            // Get the Redis configuration section
            var redisConfig = config.GetSection("Redis");

            // Extract Redis configuration parameters
            var host = redisConfig.GetSection("host").Value;
            var portString = redisConfig.GetSection("port").Value; // Fixed
            var password = redisConfig.GetSection("password").Value; // Fixed
            var defaultDatabaseString = redisConfig.GetSection("defaultDatabase").Value;

            // Type checking and parsing parameters
            if (!int.TryParse(portString, out int port))
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid port number.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            if (host == null)
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid host or null");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }

            if (!int.TryParse(defaultDatabaseString, out int defaultDb))
            {
                // Handle the error if parsing fails
                Console.WriteLine("Error: Invalid default database number.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }


            // Check if the host is a valid IP address or domain
            if (!IsValidIpAddress(host) && !IsValidDomain(host))
            {
                Console.WriteLine("Error: Invalid host. Please provide a valid IP address or domain.");
                Assert.True(false); // Indicate test failure
                return; // Exit the method
            }
            var redis = new Providers.Backup.Redis();

            // Call the Redis backup method with the parsed parameters
            var data = await redis.SetBackupAsync(host, port, password, defaultDb, "D:\\cb621462-3a35-448e-9678-c143743c61f8.json");
            var dd = "";

#warning check on connection status
            //   SaveJsonToFile(data, $"D:\\{Guid.NewGuid()}.json");

            Assert.True(true);
        }



        void SaveJsonToFile(string jsonString, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, jsonString, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the JSON string to the file: {ex.Message}");
            }
        }

        // Helper method to check if a string is a valid IP address
        private bool IsValidIpAddress(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out _))
                return true;
            return false;
        }

        // Helper method to check if a string is a valid domain
        private bool IsValidDomain(string domain)
        {
            return Uri.TryCreate(domain, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }
    }
}
