using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Tour
    {
        public string Actual
        {
            get
            {
                if (IsActual == true) { return "Актуален"; }
                else { return "Не актуален"; }
            }
        }
        public SolidColorBrush ActualColor
        {
            get
            {
                if (IsActual == true) { return Brushes.Green; }
                else { return Brushes.Red; }
            }
        }
    }
}
