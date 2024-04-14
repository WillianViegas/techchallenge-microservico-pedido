namespace techchallenge_microservico_pedido.Models
{
    public class Produto
    {
        public string Id { get; set; }
        public string? Nome { get; set; } = null;
        public string? Descricao { get; set; } = null;
        public decimal Preco { get; set; }
        public string? CategoriaId { get; set; } = null;
    }
}
