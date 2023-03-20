using apiRestKyoInfoEmpresas.Controllers;
using apiRestKyoInfoEmpresas.Models;
using apiRestKyoInfoEmpresas.Models.DTO;
using apiRestKyoInfoEmpresas.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using static MongoDB.Driver.WriteConcern;

namespace apiRestKyoInfoEmpresas.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;
        private readonly IMongoCollection<BsonDocument> _usersCollection;
        private readonly ILogger<AuthenticationController> logger;        

        public UsuarioService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings, ILogger<AuthenticationController> logger )
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _usuariosCollection = mongoDatabase.GetCollection<Usuario>(bookStoreDatabaseSettings.Value.UsuariosCollectionName);
            _usersCollection = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName).GetCollection<BsonDocument>(bookStoreDatabaseSettings.Value.UsuariosCollectionName);

            this.logger = logger;            
        }

        public async Task<List<Usuario>> GetUsuarioPorFecha(DateTime fecha1, DateTime fecha2)
        {
            var filterBuilder1 = Builders<Usuario>.Filter;
            var filter1 = filterBuilder1.Gte(x => x.FechaCreacion, fecha1);
            var filter2 = filterBuilder1.Lte(x => x.FechaCreacion, fecha2);

            //var resp = await _usuariosCollection.Find(filter1 & filter2).ToListAsync();
            var resp = await _usuariosCollection.Find(x => x.FechaCreacion >= fecha1 & x.FechaCreacion <= fecha2)
                .ToListAsync();
            return resp;

        }

        public async Task<List<Usuario>> GetAsync()
        {
            var equalPilotsFilter = Builders<Usuario>.Filter.Empty;
            var simpleProjection = Builders<Usuario>.Projection.Exclude(u => u.Password).Exclude(u => u.Empresa).Exclude(u => u.Perfil);
            
            var customProjection = Builders<Usuario>.Projection.Expression(u =>
                    new
                    {
                        id = u.Id,
                        email = u.Email,
                        nombre = u.Descripcion,
                        estado = u.Estado,
                        empresa = u.EmpresaId,
                        perfil = u.PerfilId
                    });

            var respuser = await _usuariosCollection                
                .Find(equalPilotsFilter)                
                .Project(customProjection)
                .ToListAsync();

            List<Usuario> lstUser = new List<Usuario>();
            foreach (var item in respuser)
            {                
                lstUser.Add(new Usuario()
                {
                    Id = item.id,
                    Email = item.email,
                    Descripcion = item.nombre,
                    Estado = item.estado,
                    EmpresaId = item.empresa,
                    PerfilId = item.perfil
                });
            }

            return lstUser;
        }
            

        public async Task<Usuario?> GetAsync(string id) =>
            await _usuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Usuario usuario)
        {
            await _usuariosCollection.InsertOneAsync(usuario);
        }
            

        public async Task UpdateAsync(string id, Usuario updatedUsuario) =>
            await _usuariosCollection.ReplaceOneAsync(x => x.Id == id, updatedUsuario);

        public async Task RemoveAsync(string id) =>
            await _usuariosCollection.DeleteOneAsync(x => x.Id == id);
        
        public async Task<LoginResponseDto> GetByEmail(string email, string pwd)
        {
            LoginResponseDto loginresponse = new LoginResponseDto();

            try
            {               

                var equalPilotsFilter = Builders<Usuario>.Filter.Eq(u => u.Email, email);
                var equalPilotsFilter2 = Builders<Usuario>.Filter.Eq(u => u.Password, pwd);
                
                var agg2 = await _usuariosCollection.Aggregate()
                    .Match(equalPilotsFilter & equalPilotsFilter2)                    
                    .Lookup("perfil","Perfil","_id","xperfil")
                    .Unwind("xperfil")
                    //.Project(simpleProjection)
                    .Project(Builders<BsonDocument>.Projection.Exclude("_id").Exclude("Empresa").Exclude("Perfil").Exclude("xperfil._id"))
                    .ToListAsync();

                String ConvertToJsonAGG = agg2[0].AsBsonDocument.ToJson();

                //esta forma tengo que tener el dto listo
                //var xres = System.Text.Json.JsonSerializer.Deserialize<Usuario>(ConvertToJsonAGG);

                // esta forma es anonimo pero necesito el dto para despues
                var definition = new { Email="", Descripcion = "", EmpresaId = "", PerfilId = "", xperfil = new { Nombre = "", Descripcion="" } };
                var userResp = JsonConvert.DeserializeAnonymousType(ConvertToJsonAGG, definition);


                loginresponse.Correo = userResp.Email;
                loginresponse.Nombre = userResp.Descripcion;
                loginresponse.Perfil = userResp.xperfil.Nombre;
                loginresponse.PerfilId = userResp.PerfilId;
                loginresponse.Estado = true;

                logger.LogInformation($"Response  Login: {JsonConvert.SerializeObject(loginresponse)}");

            }
            catch (Exception ex)
            {
                logger.LogError($"Error Login: {JsonConvert.SerializeObject(ex)}");
                loginresponse.Estado = false;
                return loginresponse;
            }            
                        
            return loginresponse;

        }

        private void ejemploBuenoo(string email, string pwd)
        {
            List<BsonDocument> pResults = _usersCollection
                    .Aggregate()
                    .Match(new BsonDocument { { "Email", email }, { "Password", pwd } })
                    .Lookup("perfil", "Perfil", "_id", "xperfil")
                    .Project(Builders<BsonDocument>.Projection.Exclude("_id"))
                    .Unwind("xperfil")
                    .ToList();
            String ConvertToJson = pResults[0].AsBsonDocument.ToJson();
            String resultsConvertToJson = ConvertToJson.ToJson();
            //List<TModel> results = BsonSerializer.Deserialize<List<TMModel>>(resultsConvertToJson);
            logger.LogInformation($"Resultado  bson Lookup {JsonConvert.SerializeObject(resultsConvertToJson)}");
        }
            
    }
}

