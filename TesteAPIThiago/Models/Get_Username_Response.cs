using System.Text.Json.Serialization;

namespace TesteAPIThiago.Models
{
    class Get_Username_Response
    {
        [JsonPropertyName("username")]
        public string username { get; set; }
    }
}
