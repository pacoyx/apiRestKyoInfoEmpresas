using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiRestKyoInfoEmpresas.Models
{
    public class Empleado
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; } = string.Empty;

        [BsonElement("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [BsonElement("Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [BsonElement("tipo_doc")]
        public string TipoDoc { get; set; } = string.Empty;

        [BsonElement("num_doc")]
        public string NumDoc { get; set; } = string.Empty;

        [BsonElement("telefono")]
        public string telefono { get; set; } = string.Empty;

        [BsonElement("direccion")]
        public string direccion { get; set; } = string.Empty;

        [BsonElement("estado")]
        public bool Estado { get; set; }
    }
}
