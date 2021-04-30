// ******************************
// Article BlazorSpread
// ******************************
using BlazorCosmosDB.Shared;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorCosmosDB.Server.Services
{
    public static class SeedData
    {
        public static async Task<List<T>> GetDataSample<T>(ICosmosService<T> _service)
        {
            var file = $"{Startup.PATH}/Statics/{typeof(T).Name}_SEED.json";
            if (File.Exists(file)) {
                var data = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(file));
                foreach (T item in data) {
                    await _service.AddItemAsync(item);
                }
                return data;
            }
            return new List<T>();
        }
    }
}
