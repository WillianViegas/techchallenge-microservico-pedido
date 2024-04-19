using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido_test
{
    public class PedidoTests
    {
        [Fact]
        public async Task CreatePedidoFromCarrinho()
        {
            //arrange
            var carrinho = GetCarrinhoObj();
            var pedido = GetPedidoObj();

            var carrinhoService = new Mock<ICarrinhoService>().Object;
            var pedidoService = new Mock<IPedidoService>().Object;

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedidoFromCarrinho(It.IsAny<Carrinho>()))
                .ReturnsAsync(pedido);

            //act
            var result = await pedidoService.CreatePedidoFromCarrinho(carrinho);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreatePedido()
        {
            //arrange
            var pedido = GetPedidoObj();
            var pedidoService = new Mock<IPedidoService>().Object;

            Mock.Get(pedidoService)
                .Setup(service => service.CreatePedido(It.IsAny<Pedido>()))
                .ReturnsAsync(pedido);

            //act
            var result = await pedidoService.CreatePedido(pedido);

            //assert
            Assert.NotNull(result);
        }


        private Pedido GetPedidoObj()
        {
            var produtos = new List<Produto>();
            var produto = new Produto()
            {
                Id = "",
                Nome = "",
                Descricao = "",
                CategoriaId = "",
                Preco = 10.00m
            };

            produtos.Add(produto);


            var pedido = new Pedido();
            pedido.Id = "";
            pedido.IdCarrinho = pedido.IdCarrinho;
            pedido.Numero = 1;
            pedido.DataCriacao = DateTime.Now;
            pedido.Produtos = produtos;
            pedido.Status = EPedidoStatus.Novo;

            return pedido;
        }

        private Carrinho GetCarrinhoObj()
        {
            var produtos = new List<Produto>();
            var produto = new Produto()
            {
                Id = "",
                Nome = "",
                Descricao = "",
                CategoriaId = "",
                Preco = 10.00m
            };

            produtos.Add(produto);


            var carrinho = new Carrinho()
            {
                Id = "",
                Usuario = null,
                Ativo = true,
                Produtos = produtos,
                Total = 10.00m
            };

            return carrinho;
        }
    }
}