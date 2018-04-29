using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMCalc.Common
{
    public class ConfirmAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            // MessageEventArgs以外の場合は何もしない
            var args = parameter as MessageEventArgs;
            if (args == null)
            {
                return;
            }

            // メッセージボックスを表示
            var result = MessageBox.Show(
                args.Message.Body.ToString(),
                "確認",
                MessageBoxButton.OKCancel
                );

            // ボタンの結果をResponseに格納してコールバックを呼ぶ
            args.Message.Response = (result == MessageBoxResult.OK);
            args.Callback(args.Message);
        }

    }
}
