using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace apiRestKyoInfoEmpresas.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password es requerido")]
        public string Password{ get; set; } = null!;

        public string Descripcion{ get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Empresa es requerido")]
        public string Empresa { get; set; } = null!;                

        public int Estado { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "Perfil es requerido")]
        public string Perfil { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("empresa_id")]
        [Required(ErrorMessage = "Empresa es requerido")]
        public string EmpresaId { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("perfil_id")]
        [Required(ErrorMessage = "Perfil es requerido")]
        public string PerfilId { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

    }
}
