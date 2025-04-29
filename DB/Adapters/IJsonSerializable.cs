using System.Reflection;
using System.Text.Json.Nodes;

namespace Fiap.Agnello.CLI.db.Adapters
{
    internal abstract class IJsonSerializable
    {
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
    }
}
