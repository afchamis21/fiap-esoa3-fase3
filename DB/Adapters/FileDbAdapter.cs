using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace Fiap.Agnello.CLI.db.Adapters
{
    /// <summary>
    /// Adaptador de banco de dados baseado em arquivos JSON.
    /// Permite salvar e carregar dados serializados a partir de um arquivo específico.
    /// </summary>
    /// <typeparam name="T">O tipo dos objetos a serem armazenados. Deve herdar de <see cref="IJsonSerializable"/>.</typeparam>
    /// <typeparam name="ID">O tipo da chave identificadora usada no dicionário. Deve ser não-nula.</typeparam>

    internal class FileDbAdapter<T, ID> where ID : notnull where T : IJsonSerializable
    {
        private readonly string _path;

        /// <summary>
        /// Inicializa um novo adaptador de banco de dados de arquivos.
        /// Cria o diretório base se ele não existir.
        /// </summary>
        /// <param name="tableName">Nome da tabela. O arquivo será criado no formato {tableName}.db.json</param>

        public FileDbAdapter(string tableName)
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "DB");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (tableName.Contains('.'))
            {
                throw new ArgumentException("O adapter deve receber apenas o nome da tabela. Ex: user");
            }

            _path = Path.Combine(dir, tableName + ".db.json");
        }

        /// <summary>
        /// Carrega os dados do arquivo JSON como um dicionário de objetos.
        /// </summary>
        /// <returns>Dicionário de objetos desserializados ou null em caso de erro ou arquivo vazio.</returns>
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

        /// <summary>
        /// Salva o dicionário de objetos no arquivo JSON especificado.
        /// </summary>
        /// <param name="dict">Dicionário a ser salvo.</param>
        /// <returns>True se a operação for bem-sucedida; caso contrário, false.</returns>
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
