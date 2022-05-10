using System.Text.Json.Serialization;

namespace BlogPessoalVS.src.utilidades
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}
