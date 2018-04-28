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
        private double lhs;
        public double Lhs
        {
            get { return this.lhs; }
            set
            {
                this.lhs = value;
                this.RaisePropertyChanged(nameof(Lhs));
            }
        }
        // 右辺
        private double rhs;
        public double Rhs
        {
            get { return this.rhs; }
            set
            {
                this.rhs = value;
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
            this.Answer = calc.Execute(this.Lhs, this.Rhs, this.SelectedCalculateType.CalculateType);
        }
        // 実行可否判定
        private bool CanCalculateExecute()
        {
            // 「未選択」以外なら実行OK
            return this.SelectedCalculateType.CalculateType != CalculateType.None;
        }

    }
}
