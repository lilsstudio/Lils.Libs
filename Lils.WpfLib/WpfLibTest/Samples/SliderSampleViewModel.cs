using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Mvvm;

namespace WpfLibTest.Samples
{
    public class SliderSampleViewModel : BindableBase
    {
        public double RedValuePercent { get; set; }

        public byte RedValue => Convert.ToByte(RedValuePercent * 256);

        public Color GreenMax => Color.FromRgb(RedValue, 255, 255);

        public Color GreenMin => Color.FromRgb(RedValue, 0, 255);
    }
}
