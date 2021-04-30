// ******************************
// Article BlazorSpread
// ******************************
using System.Globalization;

namespace BlazorCosmosDB.Server.Services
{
    public static class Utils
    {
        public const string DEFAULT_PARTITION = "COUNTRYID";

        static string _country;
        public static string COUNTRYID {
            get {
                if (_country == null) {
                    _country = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;
                }
                return _country;
            }
        }

        // reflexion for get string value oj an object by name
        public static string GetValue<T>(T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, default)?.ToString();
        }

        // reflexion for set string value oj an object by name
        public static void SetValue<T>(T item, string propertyName, string value)
        {
            item.GetType().GetProperty(propertyName).SetValue(item, value, default);
        }

    }
}
