namespace apiRestKyoInfoEmpresas.Utils
{
    public interface IHelpTools
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
