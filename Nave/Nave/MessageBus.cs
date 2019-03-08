using System.Collections.Generic;

namespace Nave
{
    public class MessageBus
    {
        List<Message> messages;

        /// <summary>
        /// Construtor MessageBus
        /// </summary>
        public MessageBus()
        {
            // lista de mensagens armazenadas
            messages = new List<Message>();  
        }

        /// <summary>
        /// Método que adiciona uma nova mensagem à lista de mensagens
        /// </summary>
        /// <param name="message">mensagem a ser adicionada. Variável do tipo Message</param>
        public void MessageBusAdd( Message message)
        {
            messages.Add(message);
        }


        /// <summary>
        /// Método que envia as mensagens necessárias e apaga-as após envio
        /// </summary>
        public void MessageBusDispatch()
        {
            int i = 0;
            dynamic objeto;
            Message msg;

            //Ciclo FOR que percorre a lista de mensagens
            for (i = 0; i < this.messages.Count; i++)
            {
                msg = this.messages[i];

                //Verifica se na posição i existe mensagem
                if (msg != null)
                {
                    //Guarda na variável objeto o destinatário da mensagem
                    objeto = msg.To;

                    //Se o destinatário existir, envia a mensagem
                    if (objeto != null)
                    {
                        objeto.onMessage(msg);
                    }

                    //Após realizar todas as ações, apaga a mensagem
                    this.messages.RemoveAt(i);
                }
            }
        }

    }
}
