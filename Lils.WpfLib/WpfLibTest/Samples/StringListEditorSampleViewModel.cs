using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WpfLibTest.Samples
{
    public class StringListEditorSampleViewModel : BindableBase
    {
        public StringListEditorSampleViewModel()
        {
            var stringList = new string[] {
                "string1", "string2", "string3", "string1","string2", "string1"
            };
            StringCollection = new ObservableCollection<string>(stringList);
        }

        public ObservableCollection<string> StringCollection { get; set; }
    }
}
