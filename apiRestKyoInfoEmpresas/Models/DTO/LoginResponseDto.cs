namespace apiRestKyoInfoEmpresas.Models.DTO
{
    public class LoginResponseDto
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Perfil { get; set; }
        public string PerfilId { get; set; }

        public bool Estado { get; set; }
    }
}
