using apiRestKyoInfoEmpresas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace apiRestKyoInfoEmpresas.Services
{
    public class EmpleadoService
    {
        private readonly IMongoCollection<Empleado> _empleadoCollection;
        public EmpleadoService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(bookStoreDatabaseSettings.Value.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _empleadoCollection = mongoDatabase.GetCollection<Empleado>(bookStoreDatabaseSettings.Value.EmpleadoCollectionName);
        }

        public async Task<List<Empleado>> GetAsync() => await _empleadoCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        public async Task<Empleado?> GetAsync(string id) => await _empleadoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Empleado newEmpleado) => await _empleadoCollection.InsertOneAsync(newEmpleado);

        public async Task UpdateAsync(string id, Empleado updatedEmpleado) =>
           await _empleadoCollection.ReplaceOneAsync(x => x.Id == id, updatedEmpleado);

        public async Task RemoveAsync(string id) =>
            await _empleadoCollection.DeleteOneAsync(x => x.Id == id);

    }
}
