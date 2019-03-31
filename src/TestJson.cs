using Newtonsoft.Json.Linq;

namespace src
{

    //https://www.newtonsoft.com/json/help/html/ParsingLINQtoJSON.htm
    public class TestJson
    {
        public JObject ReadJsonFromString(string json)
        {
            return JObject.Parse(json);
        }
    }
}