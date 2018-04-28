using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMCalc.Common
{
    public class DelegateCommand : ICommand
    {
        // 実行するコマンドの内容
        private Action execute;
        // コマンドが実行可能であるかを判定する処理
        private Func<bool> canExecute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }
        // canExecute省略バージョンのコンストラクタ。常に実行可。
        public DelegateCommand(Action execute) : this(execute, () => true)
        {

        }

        // コマンドの実行
        void ICommand.Execute(object parameter)
        {
            this.Execute();
        }
        public void Execute()
        {
            this.execute();
        }

        // コマンドの実行可否判定
        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }
        public bool CanExecute()
        {
            return this.canExecute();
        }

        // コマンドの実行可否状態が変化した際に発火されるイベント
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
