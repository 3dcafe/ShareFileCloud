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
        /// <summary>
        /// Get string from the local database
        /// </summary>
        /// <param name="key">Key object</param>
        private async Task<string?> GetStringAsync(string key)
        {
            var value = await _db.GetDatabase().StringGetAsync(key);
            return value;
        }
    }
}
