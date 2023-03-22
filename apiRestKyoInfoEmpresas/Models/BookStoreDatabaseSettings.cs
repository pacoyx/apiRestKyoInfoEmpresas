namespace apiRestKyoInfoEmpresas.Models
{
    public class BookStoreDatabaseSettings : IBookstoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsuariosCollectionName { get; set; } = null!;
        public string EmpresasCollectionName { get; set; } = null!;
        public string GuiaCollectionName { get; set; } = null!;
        public string PerfilCollectionName { get; set; } = null!;
        public string EmpleadoCollectionName { get; set; } = null!;
        public string VehiculoCollectionName { get; set; } = null!;
    }


    public interface IBookstoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        string UsuariosCollectionName { get; set; }
        string EmpresasCollectionName { get; set; }
        string GuiaCollectionName { get; set; }
        string PerfilCollectionName { get; set; }
        string EmpleadoCollectionName { get; set; }
        string VehiculoCollectionName { get; set; }


    }

}
