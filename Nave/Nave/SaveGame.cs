using System;
using Newtonsoft.Json;
using System.IO;

namespace Nave
{
    /// <summary>
    /// Struct para fazer Save Game / Load Game
    /// </summary>
    [Serializable]
    public struct SaveGame
    {
        //Propriedades
        private string NomePlayer { get; set; }
        public int HiScore { get; set; } 
        public DateTime Data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Nave Nave { get; set; }
    }

    public class SaveGameStorage
    {
        /// <summary>
        /// Método que permite salvar o estado do jogo
        /// </summary>
        /// <param name="sg">Variável do tipo SaveGame que permite guardar os dados do jogo</param>
        public void Save(SaveGame sg)
        {
            //Path que aponta para o save game file
            string filename = Path.Combine(Directory.GetCurrentDirectory() + "/SaveGameFolder", "savegame.json");

            // Abre o ficheiro para escrita
            StreamWriter stream = new StreamWriter(filename);

            //Variável que controla a serialização do objeto em json para ser escrito para o ficheiro
            JsonSerializer serializer = new JsonSerializer();

            //Ignora valores Nulls
            serializer.NullValueHandling = NullValueHandling.Ignore;

            //Escreve a stream para o ficheiro
            using (JsonWriter writer = new JsonTextWriter(stream))
            {
                serializer.Serialize(writer, sg);
            }

            // Fecha a conexão
            stream.Close();
            
        }

        /// <summary>
        /// Método que permite fazer o Load do jogo
        /// </summary>
        /// <returns>Retorna a informação que fora previamente guardada no save file</returns>
        public SaveGame Load()
        {
            //Variável que guardará a informação armazenada no Savefile
            SaveGame info = new SaveGame();

            //Path que aponta para o save game file
            string filename = Path.Combine(Directory.GetCurrentDirectory() + "/SaveGameFolder", "savegame.json");

            //Verificação da existência do ficheiro de save
            if (!File.Exists(filename))
                //No caso do ficheiro não existir será retornada a variável info vazia         
                return info;

            //Try ~ Catch
            try
            {
                // Cria uma instância de StreamReader para ler o ficheiro de save game
                using (StreamReader stream = new StreamReader(filename))
                {
                    // Array de chars para funcionar como buffer
                    Char[] buffer;

                    //Leitura do ficheiro
                    buffer = new Char[(int)stream.BaseStream.Length];
                    stream.Read(buffer, 0, (int)stream.BaseStream.Length);


                    // Lê os dados do buffer (texto) que se encontra em formato json e transforma a informação num objeto SaveGame
                    info = JsonConvert.DeserializeObject<SaveGame>(buffer.ToString());

                    // Fecha a coexão ao ficheiro
                    stream.Close();
                }
               
            }
            catch (Exception e)
            {
                // Debug
                Console.WriteLine("Não foi possível ler o ficheiro:");
                Console.WriteLine(e.Message);
            }
            
            //Retorna valores guardados no save file
            return info;
        }
    }
}