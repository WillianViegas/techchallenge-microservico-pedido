﻿namespace techchallenge_microservico_pedido.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string? Tipo { get; set; } = null;
        public string? Senha { get; set; } = null;
    }
}
