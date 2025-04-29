using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Menu.Domain;

namespace Fiap.Agnello.CLI.db.Adapters
{
    internal class FileDbAdapter<T, ID> where ID : notnull where T : IJsonSerializable
    {
        private readonly string _path;

        public FileDbAdapter(string path)
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "DB");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            _path = Path.Combine(dir, path);
        }

        public Dictionary<ID, T>? LoadFromFile()
        {
            try
            {
                if (File.Exists(_path))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    Dictionary<ID, T>? dict = JsonSerializer.Deserialize<Dictionary<ID, T>>(File.ReadAllText(_path));
                    if (dict != null)
                    {
                        return dict;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR --- Failed to load file [{_path}]: {ex.Message}");
                Console.ResetColor();
            }

            return null;
        }

        public bool SaveToFile(Dictionary<ID, T> dict)
        {
            try
            {
                JsonObject root = [];

                foreach (var item in dict)
                {
                    T value = item.Value;
                    ID key = item.Key;

                    root[key.ToString()] = value.ToJsonObject();
                }


                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                };

                Console.WriteLine($"Saving to: {Path.GetFullPath(_path)}");

                File.WriteAllText(_path, root.ToJsonString(options));
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR --- Error saving db file contents to file [{_path}]: {ex.Message}");
                Console.ResetColor();
                return false;
            }
        }
    }
}
