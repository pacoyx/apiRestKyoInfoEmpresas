using apiRestKyoInfoEmpresas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace apiRestKyoInfoEmpresas.Services
{
    public class PerfilService
    {
        private readonly IMongoCollection<Perfil> _perfilCollection;

        public PerfilService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings) {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(bookStoreDatabaseSettings.Value.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;            
            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _perfilCollection = mongoDatabase.GetCollection<Perfil>(bookStoreDatabaseSettings.Value.PerfilCollectionName);
        }


        public async Task<List<Perfil>> GetAsync() =>
           await _perfilCollection.Find(_ => true).ToListAsync();

        public async Task<Perfil?> GetAsync(string id) =>
            await _perfilCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Perfil newPerfil) =>
            await _perfilCollection.InsertOneAsync(newPerfil);

        public async Task UpdateAsync(string id, Perfil updatedPerfil) =>
            await _perfilCollection.ReplaceOneAsync(x => x.Id == id, updatedPerfil);

        public async Task RemoveAsync(string id) =>
            await _perfilCollection.DeleteOneAsync(x => x.Id == id);


    }
}
