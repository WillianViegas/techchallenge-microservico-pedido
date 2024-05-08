using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido_test.Services
{
    public class PedidoTests
    {
        [Fact]
        public async Task GetCarrinhoById()
        {
            var carrinhoService = new Mock<ICarrinhoService>().Object;
            var carrinho = GetCarrinhoObj();
            var idCarrinho = Guid.NewGuid().ToString();
            carrinho.Id = idCarrinho;

            Mock.Get(carrinhoService)
              .Setup(rep => rep.GetCarrinhoById(idCarrinho))
                .ReturnsAsync(carrinho);

            //act
            var result = await carrinhoService.GetCarrinhoById(idCarrinho);

            //assert
            Assert.NotNull(result);
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