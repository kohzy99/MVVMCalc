using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Model
{
    public class Calculator
    {
        private static readonly Dictionary<CalculateType, Func<double, double, double>> calcMap = new Dictionary<CalculateType, Func<double, double, double>>()
        {
            // 未指定
            {
                CalculateType.None,
                (x,y) => { throw new InvalidOperationException(); }
            },
            // 足し算
            {
                CalculateType.Add,
                (x,y) => { return x + y; }
            },
            // 引き算
            {
                CalculateType.Sub,
                (x,y) => { return x - y; }
            },
            // 掛け算
            {
                CalculateType.Mul,
                (x,y) => { return x * y; }
            },
            // 割り算
            {
                CalculateType.Div,
                (x,y) => { return x / y; }
            },
        };

        public double Execute(double x,double y,CalculateType op)
        {
            return calcMap[op](x, y);
        }
    }
}
