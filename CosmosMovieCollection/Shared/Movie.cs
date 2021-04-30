// ******************************
// Article BlazorSpread
// ******************************
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BlazorCosmosDB.Shared
{
    public class Movie
    {
        // CosmosDB requires id in container 
        // CosmosDB uses Newtonsoft
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public string ISBN { get; set; }

        [JsonPropertyName("director")]
        public string Director{ get; set; }

        //[JsonPropertyName("language")]
        //public string Language { get; set; }

        [JsonPropertyName("posterUrl")]
        public string Link { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("runtime")]
        public string Runtime { get; set; }

        [JsonPropertyName("plot")]
        public string Plot { get; set; }

        [JsonPropertyName("genres")]
        public string Genres { get; set; }

        [JsonPropertyName("actors")]
        public string Actors { get; set; }
        
        // ** PartitionKey: automated setting the server country 
        public string Partition { get; set; }

        public override string ToString() => $"{Title}, {Director}";
    }
}
