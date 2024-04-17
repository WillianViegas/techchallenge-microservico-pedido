using MongoDB.Driver;
using techchallenge_microservico_pedido.DatabaseConfig;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;
using techchallenge_microservico_pedido.Services.Interfaces;

namespace techchallenge_microservico_pedido.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly IMongoCollection<Carrinho> _collection;

        public CarrinhoRepository(IDatabaseConfig databaseConfig)
        {
            var connectionString = databaseConfig.ConnectionString.Replace("user", databaseConfig.User).Replace("password", databaseConfig.Password);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);
            _collection = database.GetCollection<Carrinho>("Carrinho");
        }

        public async Task<Carrinho> GetCarrinhoById(string id)
        {
            return await _collection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }
    }
}
