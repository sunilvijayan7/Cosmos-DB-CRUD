// ******************************
// Article BlazorSpread
// ******************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDB.Server.Services
{
    /// <summary>
    /// Generic CosmosDB data handler
    /// </summary>
    public class CosmosService<T> : ICosmosService<T>
    {
        readonly Container _container;
        readonly string _partitionName;

        public CosmosService(Container container, string partitionName)
        {
            _container = container;
            _partitionName = partitionName;
        }

        public async Task<bool> AddItemAsync(T item)
        {
            try {
                var response = await _container.CreateItemAsync(item, GetPartitionKey(item));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<T> GetItemAsync(string id, string partition)
        {
            try {
                // works due to the id is unique
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, GetPartitionKey(partition));
                return response.Resource;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return default;
        }

        public async Task<bool> DeleteItemAsync(string id, string partition)
        {
            try {
                // works due to the id is unique
                await _container.DeleteItemAsync<T>(id, GetPartitionKey(partition));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var results = new List<T>();
            try {
                var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
                while (query.HasMoreResults) {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return results;
        }

        public async Task<bool> UpdateItemAsync(string id, T item)
        {
            try {
                await _container.ReplaceItemAsync(item, id, GetPartitionKey(item));
                return true;
            }
            catch (Exception exception) {
                Trace.WriteLine($"** Exception: {exception.Message}");
            }
            return false;
        }

        private PartitionKey GetPartitionKey(T item)
        {
            if (string.IsNullOrEmpty(Utils.GetValue(item, _partitionName))) {
                // solve with default
                Utils.SetValue(item, _partitionName, Utils.COUNTRYID);
            }
            return new PartitionKey(Utils.GetValue(item, _partitionName));
        }

        private static PartitionKey GetPartitionKey(string value)
        {
            if (value == Utils.DEFAULT_PARTITION || string.IsNullOrEmpty(value)) {
                // solve with default
                value = Utils.COUNTRYID;
            }
            return new PartitionKey(value);
        }
    }
}
