using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVMCalc.Common
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged 関連

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDataErrorInfo 関連

        // プロパティに紐付いたエラーメッセージを格納します
        private Dictionary<string, string> errors = new Dictionary<string, string>();

        // 指定されたキーのエラー情報が存在していれば返します
        public string this[string columnName]
        {
            get
            {
                if (this.errors.ContainsKey(columnName))
                {
                    return this.errors[columnName];
                }
                return null;
            }
        }

        // プロパティにエラーメッセージを設定します
        protected void SetError(string propertyName, string errorMessage)
        {
            this.errors[propertyName] = errorMessage;
            this.RaisePropertyChanged("HasError");
        }

        // プロパティのエラーをクリアします
        protected void ClearError(string propertyName)
        {
            if (this.errors.ContainsKey(propertyName))
            {
                this.errors.Remove(propertyName);
                this.RaisePropertyChanged("HasError");
            }
        }

        // 全てのエラーをクリアします
        protected void ClearErrors()
        {
            this.errors.Clear();
            this.RaisePropertyChanged("HasError");
        }

        // エラーの有無を取得します
        public bool HasError
        {
            get
            {
                return this.errors.Count != 0;
            }
        }

        // 未使用
        public string Error => throw new NotImplementedException();

        #endregion
    }
}
