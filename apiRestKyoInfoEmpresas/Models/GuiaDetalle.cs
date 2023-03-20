using MongoDB.Bson.Serialization.Attributes;

namespace apiRestKyoInfoEmpresas.Models
{
    public class GuiaDetalle
    {
        [BsonElement("prenda")]
        public string Prenda { get; set; } = null!;

        [BsonElement("cantidad")]
        public int Cantidad { get; set; } = 0;

        [BsonElement("peso")]
        public int Peso { get; set; } = 0;

        [BsonElement("estado")]
        public bool Estado { get; set; }

    }
}
