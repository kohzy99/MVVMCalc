using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMCalc.Common;
using MVVMCalc.Model;

namespace MVVMCalc.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public IEnumerable<CalculateTypeViewModel> CalculateTypes { get; private set; }

        // コンストラクタ
        public MainViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create();
            this.selectedCalculateType = this.CalculateTypes.First();

            // 入力値の検証を行う
            this.Lhs = string.Empty;
            this.Rhs = string.Empty;
        }

        // 現在選択されている計算タイプ
        private CalculateTypeViewModel selectedCalculateType;
        public CalculateTypeViewModel SelectedCalculateType
        {
            get { return this.selectedCalculateType; }
            set
            {
                this.selectedCalculateType = value;
                this.RaisePropertyChanged(nameof(SelectedCalculateType));
            }
        }

        // 左辺
        private string lhs;
        public string Lhs
        {
            get { return this.lhs; }
            set
            {
                this.lhs = value;
                if (!this.IsDouble(value))
                {
                    this.SetError(nameof(Lhs), "数字を入力してください");
                }
                else
                {
                    this.ClearError(nameof(Lhs));
                }
                this.RaisePropertyChanged(nameof(Lhs));
            }
        }

        // 右辺
        private string rhs;
        public string Rhs
        {
            get { return this.rhs; }
            set
            {
                this.rhs = value;
                if (!this.IsDouble(value))
                {
                    this.SetError(nameof(Rhs), "数字を入力してください");
                }
                else
                {
                    this.ClearError(nameof(Rhs));
                }
                this.RaisePropertyChanged(nameof(Rhs));
            }
        }
        // 計算結果
        private double answer;
        public double Answer
        {
            get { return this.answer; }
            set
            {
                this.answer = value;
                this.RaisePropertyChanged(nameof(Answer));
            }
        }

        // 引数がDouble型にキャストできるかチェックします
        private bool IsDouble(string value)
        {
            var temp = default(double);
            return double.TryParse(value, out temp);
        }

        // 計算処理を行うコマンド
        private DelegateCommand calculateCommand;
        public DelegateCommand CalculateCommand
        {
            get
            {
                if (this.calculateCommand == null)
                {
                    this.calculateCommand = new DelegateCommand(CalculateExecute, CanCalculateExecute);
                }
                return this.calculateCommand;
            }
        }
        private void CalculateExecute()
        {
            // 現在の入力値を元に計算を行う
            var calc = new Calculator();
            this.Answer = calc.Execute(double.Parse(this.Lhs), double.Parse(this.Rhs), this.SelectedCalculateType.CalculateType);

            if (IsInvalidAnswer())
            {
                // 計算結果が実数の範囲から外れている場合はViewに通知する
                this.ErrorMessenger.Raise(
                    new Message("計算結果が実数の範囲を超えました。入力値を初期化しますか？"),
                    m =>
                    {
                        // View から入力を初期化すると指定されて場合はプロパティの初期化を行う
                        if (!(bool)m.Response)
                        {
                            return;
                        }
                        InitializeProperties();
                    }
                    );
            }

        }

        // プロパティの初期化を行う
        private void InitializeProperties()
        {
            this.Lhs = string.Empty;
            this.Rhs = string.Empty;
            this.Answer = default(double);
            this.SelectedCalculateType = this.CalculateTypes.First();
        }

        // Answer が有効な実数値か確認する
        private bool IsInvalidAnswer()
        {
            return double.IsInfinity(this.Answer) || double.IsNaN(this.Answer);
        }

        // 実行可否判定
        private bool CanCalculateExecute()
        {
            // 「未選択」以外なら実行OK
            return this.SelectedCalculateType.CalculateType != CalculateType.None && !this.HasError;
        }

        #region Messenger 関連

        // 計算結果にエラーがあったことを通知するメッセージを送信するメッセンジャー
        private Messenger errorMessenger = new Messenger();
        public Messenger ErrorMessenger
        {
            get { return errorMessenger; }
        }

        #endregion

    }
}
