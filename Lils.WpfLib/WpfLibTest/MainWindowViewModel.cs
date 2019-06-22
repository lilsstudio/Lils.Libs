using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WpfLibTest
{
    enum EnumItem
    {
        ItemA,
        ItemB,
        ItemC,
    }

    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            Items = Enum.GetValues(typeof(EnumItem)).Cast<EnumItem>().ToList();
            SelectedItem = EnumItem.ItemC;
        }

        public List<EnumItem> Items { get; set; }
        public EnumItem SelectedItem { get; set; }

        public string SampleText { get; set; }

        public double SliderValue { get; set; }
    }
}
