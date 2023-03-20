using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace apiRestKyoInfoEmpresas.Models
{
    public class GuiaRecojo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("numero_guia")]
        public string NumeroGuia { get; set; } = null!;
                
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("empresa_id")]
        public string EmpresaId { get; set; } = null!;

        [BsonElement("area")]
        public string Area { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("fecha_guia")]
        public DateTime FechaGuia { get; set; }

        [BsonElement("total_peso")]
        public double TotalPeso { get; set; }

        [BsonElement("total_cantidad")]
        public double TotalCantidad { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("personal_id")]
        public string PersonalId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("vehiculo_id")]
        public string VehiculoId { get; set; } = null!;


        [BsonElement("personal_entrega")]
        public string PersonalEntrega { get; set; } = null!;

        [BsonElement("personal_recibe")]
        public string PersonalRecibe { get; set; } = null!;

        [BsonElement("observaciones")]
        public string Observaciones { get; set; } = null!;

        [BsonElement("estado_guia")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Estado { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [BsonElement("usuario_creacion")]
        public string UsuarioCreacion { get; set; } = null!;

        public ICollection<GuiaDetalle>? Prendas { get; set; }

    }
}
