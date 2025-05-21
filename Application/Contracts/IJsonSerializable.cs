using System.Reflection;
using System.Text.Json.Nodes;

namespace Fiap.Agnello.CLI.Application.Contracts
{
    /// <summary>
    /// Classe base abstrata que define um comportamento de serialização para objetos em formato JSON.
    /// Qualquer classe que herdar de <see cref="IJsonSerializable"/> poderá converter suas propriedades públicas legíveis em um <see cref="JsonObject"/>.
    /// </summary>
    internal abstract class IJsonSerializable
    {
        /// <summary>
        /// Converte todas as propriedades públicas legíveis do objeto em um <see cref="JsonObject"/>.
        /// Propriedades que começam com "_" são ignoradas.
        /// </summary>
        /// <returns>Um <see cref="JsonObject"/> contendo as propriedades do objeto.</returns>
        public JsonObject ToJsonObject()
        {
            JsonObject json = [];
            PropertyInfo[] props = GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.CanRead && !prop.Name.StartsWith("_"))
                {
                    object? value = prop.GetValue(this);
                    json[prop.Name] = JsonValue.Create(value);
                }
            }

            return json;
        }

        public String ToJsonString()
        {
            return ToJsonObject().ToString();
        }
    }
}
