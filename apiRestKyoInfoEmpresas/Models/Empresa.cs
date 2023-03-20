using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiRestKyoInfoEmpresas.Models
{
    public class Empresa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; } = null!;
        [BsonElement("ruc")]
        public string Ruc { get; set; } = null!;

        [BsonElement("estado")]
        public bool Estado { get; set; }
    }
}
