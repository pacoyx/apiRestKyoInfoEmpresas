using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apiRestKyoInfoEmpresas.Models
{
    public class GuiaCliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("numero_guia_recojo")]
        public string NumeroGuiaRecojo { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("empresa_id")]
        public string EmpresaId { get; set; } = null!;


        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("fecha_guia")]
        public DateTime FechaGuia { get; set; }

        [BsonElement("numero_guia_cliente")]
        public string NumeroGuiaCliente { get; set; } = null!;

        [BsonElement("nombre_trabajador")]
        public string NombreTrabajador { get; set; } = null!;

        [BsonElement("area")]
        public string Area { get; set; } = null!;

        [BsonElement("observaciones")]
        public string Observaciones { get; set; } = null!;

        [BsonElement("estado_guia")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Estado { get; set; }

        public ICollection<GuiaDetalle>? Prendas { get; set; }
    }
}
