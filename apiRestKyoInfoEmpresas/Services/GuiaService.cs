using apiRestKyoInfoEmpresas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace apiRestKyoInfoEmpresas.Services
{
    public class GuiaService
    {
        private readonly IMongoCollection<GuiaRecojo> _guiaCollection;

        public GuiaService(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            MongoClientSettings settings =MongoClientSettings.FromConnectionString(bookStoreDatabaseSettings.Value.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;

            // var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoClient = new MongoClient(settings);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _guiaCollection = mongoDatabase.GetCollection<GuiaRecojo>(
                bookStoreDatabaseSettings.Value.GuiaCollectionName);
        }

        public async Task<List<GuiaRecojo>> GetAsync() =>
           await _guiaCollection.Find(_ => true).ToListAsync();

        public async Task<GuiaRecojo?> GetAsync(string id) =>
            await _guiaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(GuiaRecojo newBook) =>
            await _guiaCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, GuiaRecojo updatedBook) =>
            await _guiaCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _guiaCollection.DeleteOneAsync(x => x.Id == id);


    }
}
