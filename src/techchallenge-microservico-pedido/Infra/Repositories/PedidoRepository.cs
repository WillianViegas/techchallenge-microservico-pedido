using MongoDB.Driver;
using techchallenge_microservico_pedido.DatabaseConfig;
using techchallenge_microservico_pedido.Models;
using techchallenge_microservico_pedido.Repositories.Interfaces;

namespace techchallenge_microservico_pedido.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _collection;

        public PedidoRepository(IDatabaseConfig databaseConfig)
        {
            var connectionString = databaseConfig.ConnectionString.Replace("user", databaseConfig.User).Replace("password", databaseConfig.Password);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);
            _collection = database.GetCollection<Pedido>("Pedido");
        }

        public async Task<Pedido> CreatePedido(Pedido pedido)
        {
            await _collection.InsertOneAsync(pedido);
            return pedido;
        }

        public async Task<IList<Pedido>> GetAllPedidos()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
