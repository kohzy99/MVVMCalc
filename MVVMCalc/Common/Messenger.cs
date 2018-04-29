using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Common
{
    public class Messenger
    {
        /// <summary>
        /// メッセージが送信されたことを通知するイベント
        /// </summary>
        public event EventHandler<MessageEventArgs> Raised;

        /// <summary>
        /// 指定したメッセージとコールバックでメッセージを送信する
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        public void Raise(Message message, Action<Message> callback)
        {
            var h = this.Raised;
            if (h != null)
            {
                h(this, new MessageEventArgs(message, callback));
            }
        }
    }
}
