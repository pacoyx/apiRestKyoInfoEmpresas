using apiRestKyoInfoEmpresas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace apiRestKyoInfoEmpresas.Services
{
    public class VehiculoService
    {
        private readonly IMongoCollection<Vehiculo> _vehiculoCollection;
        public VehiculoService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(bookStoreDatabaseSettings.Value.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _vehiculoCollection = mongoDatabase.GetCollection<Vehiculo>(bookStoreDatabaseSettings.Value.VehiculoCollectionName);
        }

        public async Task<List<Vehiculo>> GetAsync() => await _vehiculoCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        public async Task<Vehiculo?> GetByIdAsync(string id) => await _vehiculoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Vehiculo newEmpleado) => await _vehiculoCollection.InsertOneAsync(newEmpleado);

        public async Task UpdateAsync(string id, Vehiculo updatedvehiculo) =>
           await _vehiculoCollection.ReplaceOneAsync(x => x.Id == id, updatedvehiculo);

        public async Task RemoveAsync(string id) =>
            await _vehiculoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
