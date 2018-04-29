using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Common
{
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 送信するメッセージ
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// ViewModelのコールバック
        /// </summary>
        public Action<Message> Callback { get; private set; }

        /// <summary>
        /// コンストラクタでMessageとCallbackを受け取る
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        public MessageEventArgs(Message message, Action<Message> callback)
        {
            this.Message = message;
            this.Callback = callback;
        }
    }
}
