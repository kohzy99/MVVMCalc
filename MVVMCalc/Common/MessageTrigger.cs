using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMCalc.Common
{
    public class MessageTrigger : System.Windows.Interactivity.EventTrigger
    {
        protected override string GetEventName()
        {
            //return base.GetEventName();
            return "Raised";
        }
    }
}
