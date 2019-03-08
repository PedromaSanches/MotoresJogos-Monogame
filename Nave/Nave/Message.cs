using System;

namespace Nave
{
    /// <summary>
    /// Enumerador com valores dos tipos de mensagens
    /// </summary>
    public enum MessageType
    {
        Sound,
        Controlles,
        Gameplay,
        Physics,
        Graphics,
        Camera

    }

    public class Message
    {
        Object to;
        Object from;
        MessageType type;
        string data;

        /// <summary>
        /// Construtor de Mensagens
        /// </summary>
        /// <param name="to">Destinatário da Mensagme - Objeto</param>
        /// <param name="from">Emissor da Mensagme - Objeto</param>
        /// <param name="type">Tipo da Mensagme - Enum</param>
        /// <param name="data">Mensagem em si - String</param>
        public Message (Object to, Object from, MessageType type, string data)
        {
            this.to = to;        // referência para o objeto que vai receber a mensagem
            this.from = from;    // referência para o objeto que enviou a mensagem
            this.type = type;    // tipo da mensagem
            this.data = data;    // conteudo da mensagem
        }

        //Propriedades
        public Object To { get { return this.to; } set { this.to = value; } }
        public Object From { get { return this.from; } set { this.from = value; } }
        public MessageType Type { get { return this.type; } set { this.type = value; } }
        public string Data { get { return this.data; } set { this.data = value; } }

    }
}
