using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiRestKyoInfoEmpresas.Models
{
    public class Perfil
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

    }
}
