using Newtonsoft.Json;

namespace e_construmarket_mvc.Models
{
    public class Produto
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("preco")]
        public decimal Preco { get; set; }
        [JsonProperty("marca")]
        public Marca Marca { get; set; }
    }
}
