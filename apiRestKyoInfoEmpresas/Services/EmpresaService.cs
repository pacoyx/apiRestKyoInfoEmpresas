using apiRestKyoInfoEmpresas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace apiRestKyoInfoEmpresas.Services
{
    public class EmpresaService
    {
        private readonly IMongoCollection<Empresa> _empresaCollection;

        public EmpresaService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(bookStoreDatabaseSettings.Value.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _empresaCollection = mongoDatabase.GetCollection<Empresa>(bookStoreDatabaseSettings.Value.EmpresasCollectionName);}
        public async Task<List<Empresa>> GetAsync() => await _empresaCollection.Find(_ => true).ToListAsync();
        public async Task<Empresa?> GetAsync(string id) =>  await _empresaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Empresa newEmpresa) => await _empresaCollection.InsertOneAsync(newEmpresa);
    }
}
