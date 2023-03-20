using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apiRestKyoInfoEmpresas.Models
{
    public class Vehiculo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; } = string.Empty;

        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [BsonElement("placa")]
        public string Placa { get; set; } = string.Empty;

        [BsonElement("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [BsonElement("estado")]
        public bool Estado { get; set; } 


    }
}
