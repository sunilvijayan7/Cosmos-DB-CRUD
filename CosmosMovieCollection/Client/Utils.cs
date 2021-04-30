// ******************************
// Article BlazorSpread
// ******************************
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorCosmosDB.Client
{
    public static class Utils
    {
        static readonly Random random = new Random();
        public static string RandomString(int length, bool numbers = true)
        {
            const string CHAR = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string NUMS = "0123456789";
            var mask = numbers ? NUMS : CHAR;
            return new string(Enumerable.Repeat(mask, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomColor()
        {
            return string.Format("#{0:X6}", random.Next(0x1000000));
        }

        public static double RandomNumber()
        {
            return 10.0 * random.NextDouble();
        }

        public static string NewISBN()
        {
            return 
                RandomString(1) + "-" + 
                RandomString(5, false) + "-" + 
                RandomString(3) + "-" + 
                RandomString(1);
        }

        public static async Task<bool> ResponseResult(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK) {
                var js = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(js);
            }
            return false;
        }
    }
}
