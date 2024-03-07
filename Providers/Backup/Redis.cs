using Providers.Interfaces;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace Providers.Backup
{
    public class Redis : IBackup
    {
        private ConnectionMultiplexer _db;
        /// <summary>
        /// Initializing connections
        /// </summary>
        private bool InitConnection(string host, int port, string? password, int defaultDb)
        {
            if (_db == null)
            {
                _db = ConnectionMultiplexer.Connect(
                    new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        //
                        EndPoints = { $"{host}:{port}" },
                        Password = password,
                        Ssl = false,
                        DefaultDatabase = defaultDb,
                        ConnectTimeout = 15000,
                        SyncTimeout = 15000,
                    }
                );
            }
            return true;
        }
        public async Task<string> GetBackupAsync(string host, int port, string password, int defaultDb)
        {
            var statusConnect = InitConnection(host, port, password, defaultDb);
            if(statusConnect == false)
            {
                // Handle the case when the connection is not established
                throw new InvalidOperationException("Redis connection is not established.");
            }
            // Get all keys in the selected database
            var keys = _db.GetServer($"{host}:{port}").Keys().ToArray();

            var jsonData = new Dictionary<string, string>();
            int i = 0;
            // Retrieve values associated with each key and convert to JSON
            foreach (var key in keys)
            {
                var data = await GetStringAsync(key);
                if (data != null)
                    jsonData[key.ToString()] = data;
                i++;
                Console.WriteLine($"{i}/{keys.Length}");
            }
            // Convert the dictionary to a JSON string
            var jsonString = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            return jsonString;
        }

        public async Task<bool> SetBackupAsync(string host, int port, string? password, int defaultDb, string pathToFile)
        {
            var statusConnect = InitConnection(host, port, password, defaultDb);
            if (statusConnect == false)
                return false;

            try
            {
                // Read the JSON data from the backup file
                string jsonString = await File.ReadAllTextAsync(pathToFile);

                // Deserialize JSON data into a dictionary
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                // Set each key-value pair from the JSON data to the Redis server
                foreach (var kvp in jsonData)
                {
                    await SetStringAsync(kvp.Key, kvp.Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting backup data to Redis: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Get string from the local database
        /// </summary>
        /// <param name="key">Key object</param>
        private async Task<string?> GetStringAsync(string key)
        {
            var value = await _db.GetDatabase().StringGetAsync(key);
            return value;
        }
        /// <summary>
        /// Private method for asynchronously setting a string in the Redis storage.
        /// </summary>
        /// <param name="key">The key under which the string will be saved.</param>
        /// <param name="value">The value of the string to be saved.</param>
        /// <returns>
        /// True if the operation is successfully completed; otherwise, false.
        /// </returns>
        private async Task<bool> SetStringAsync(string key, string value)
        {
            try
            {
                // Set the string value to the Redis server
                await _db.GetDatabase().StringSetAsync(key, value);
                return true;
            }
            catch (Exception ex)
            {
                // Log error message if an exception occurs during the operation
                Console.WriteLine($"Error setting string value to Redis: {ex.Message}");
                return false;
            }
        }
    }
}
