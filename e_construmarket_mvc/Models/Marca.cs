using Newtonsoft.Json;

namespace e_construmarket_mvc.Models
{
    public class Marca
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("codigoMarca")]
        public string CodigoMarca { get; set; }
    }
}
