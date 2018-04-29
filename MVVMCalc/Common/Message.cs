using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Common
{
    public class Message
    {
        /// <summary>
        ///  メッセージの本体
        /// </summary>
        public object Body { get; private set; }

        /// <summary>
        /// ViewからViewModelへのレスポンス
        /// </summary>
        public object Response { get; set; }

        /// <summary>
        /// コンストラクタでBodyを受け取る
        /// </summary>
        /// <param name="body"></param>
        public Message(object body)
        {
            this.Body = body;
        }
    }
}
