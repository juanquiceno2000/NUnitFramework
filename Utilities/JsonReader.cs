using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace NUnitFramework.Utilities
{
    public class JsonReader
    {
        public JsonReader()
        {
        }

        public string extractData(string tokenName)
        {
            var myJsonString = File.ReadAllText("Utilities/TestData.json");

            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
    }
}
