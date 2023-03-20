using apiRestKyoInfoEmpresas.Models.DTO;
using apiRestKyoInfoEmpresas.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using apiRestKyoInfoEmpresas.Controllers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace apiRestKyoInfoEmpresas.Services
{
    public class LoginService: ILoginService
    {

        private readonly IMongoCollection<Usuario> _usuariosCollection;        
        private readonly ILogger<AuthenticationController> logger;

        public LoginService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings, ILogger<AuthenticationController> logger)
        { 
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _usuariosCollection = mongoDatabase.GetCollection<Usuario>(bookStoreDatabaseSettings.Value.UsuariosCollectionName);
            this.logger = logger;
        }


        public async Task<LoginResponseDto> Login(string email, string pwd)
        {
            LoginResponseDto loginresponse = new LoginResponseDto();

            try
            {

                var equalPilotsFilter = Builders<Usuario>.Filter.Eq(u => u.Email, email);
                var equalPilotsFilter2 = Builders<Usuario>.Filter.Eq(u => u.Password, pwd);

                var agg2 = await _usuariosCollection.Aggregate()
                    .Match(equalPilotsFilter & equalPilotsFilter2)
                    .Lookup("perfil", "Perfil", "_id", "xperfil")
                    .Unwind("xperfil")
                    //.Project(simpleProjection)
                    .Project(Builders<BsonDocument>.Projection.Exclude("_id").Exclude("Empresa").Exclude("Perfil").Exclude("xperfil._id"))
                    .ToListAsync();

                String ConvertToJsonAGG = agg2[0].AsBsonDocument.ToJson();

                //esta forma tengo que tener el dto listo
                //var xres = System.Text.Json.JsonSerializer.Deserialize<Usuario>(ConvertToJsonAGG);

                // esta forma es anonimo pero necesito el dto para despues
                var definition = new { Email = "", Descripcion = "", EmpresaId = "", PerfilId = "", xperfil = new { Nombre = "", Descripcion = "" } };
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

    }

    
}
