using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibTest.Samples
{
    class ConverterSampleViewModel
    {
        public ConverterSampleViewModel()
        {
            Items = Enum.GetValues(typeof(EnumItem)).Cast<EnumItem>().ToList();
            SelectedItem = EnumItem.ItemC;
        }

        public List<EnumItem> Items { get; set; }
        public EnumItem SelectedItem { get; set; }

        public string SampleText { get; set; }

        public double SliderValue { get; set; }
    }

    enum EnumItem
    {
        ItemA,
        ItemB,
        ItemC,
    }
}
