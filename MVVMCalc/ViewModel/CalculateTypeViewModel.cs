using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMCalc.Common;
using MVVMCalc.Model;

namespace MVVMCalc.ViewModel
{
    public class CalculateTypeViewModel : ViewModelBase
    {
        // 計算方法を保持する列挙体
        public CalculateType CalculateType { get; private set; }

        // 表示名
        public string Label { get; private set; }

        // CalculateType毎の表示用ラベルのマップ
        private static Dictionary<CalculateType, string> typeLabelMap = new Dictionary<CalculateType, string>()
        {
            {CalculateType.None,"未選択" },
            {CalculateType.Add,"足し算" },
            {CalculateType.Sub,"引き算" },
            {CalculateType.Mul,"掛け算" },
            {CalculateType.Div,"割り算" },
        };

        // コンストラクタ
        public CalculateTypeViewModel(CalculateType calculateType,string label)
        {
            this.CalculateType = calculateType;
            this.Label = label;
        }

        // 計算方法からラベル名を自動的に引き当てて、ViewModelのインスタンスを生成して返すメソッド
        public static CalculateTypeViewModel Create(CalculateType type)
        {
            return new CalculateTypeViewModel(type, typeLabelMap[type]);
        }

        // CalculateTypeの全値に対応するViewModelを生成して返すメソッド
        public static IEnumerable<CalculateTypeViewModel> Create()
        {
            foreach (CalculateType e in Enum.GetValues(typeof(CalculateType)))
            {
                yield return Create(e);
            }
        }
    }
}
