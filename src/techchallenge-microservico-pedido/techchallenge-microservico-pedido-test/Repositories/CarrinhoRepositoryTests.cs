using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using techchallenge_microservico_pedido.Enums;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido_test.Repositories
{
    public class CarrinhoServiceTests
    {
        [Fact]
        public async Task GetCarrinhoById()
        {
            var carrinhoRepository = new Mock<ICarrinhoRepository>().Object;
            var carrinho = GetCarrinhoObj();
            var idCarrinho = Guid.NewGuid().ToString();
            carrinho.Id = idCarrinho;

            Mock.Get(carrinhoRepository)
              .Setup(rep => rep.GetCarrinhoById(idCarrinho))
                .ReturnsAsync(carrinho);

            //act
            var result = await carrinhoRepository.GetCarrinhoById(idCarrinho);

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