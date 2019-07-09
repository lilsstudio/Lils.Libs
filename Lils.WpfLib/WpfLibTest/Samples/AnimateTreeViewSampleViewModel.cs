using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Prism.Mvvm;

namespace WpfLibTest.Samples
{
    public class AnimateTreeViewSampleViewModel : BindableBase
    {
        public AnimateTreeViewSampleViewModel()
        {
            LoadData();
        }

        public List<Personnel> Personnels { get; set; }

        private void LoadData()
        {
            //get assembly name 
            var fullAssemblyName = typeof(MainWindowViewModel).Assembly.FullName;
            // var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = fullAssemblyName.Substring(0, fullAssemblyName.IndexOf(','));
            // get uri by assembly name
            var uri = new Uri($"pack://application:,,,/{assemblyName};component/Data/Personnel.xml", UriKind.Absolute);
            // read data form uri
            var streamReader = new StreamReader(Application.GetResourceStream(uri).Stream);
            var data = streamReader.ReadToEnd();

            Personnels =
                XElement.Parse(data)
                    .Elements()
                    .Select(e => new Personnel
                    {
                        Name = e.Attribute("Name").Value,
                        Sex = e.Attribute("Sex").Value,
                        Birth = e.Attribute("Birth").Value,
                        Phone = e.Attribute("Phone").Value,
                        Email = e.Attribute("Email").Value,
                        Address = e.Attribute("Address").Value,
                        Website = e.Attribute("Website").Value
                    })
                    .ToList();
            var first = Personnels[0];
            var second = Personnels[1];
            Personnels.RemoveRange(0, 2);
            Personnels[0].Children = new List<Personnel>
            {
                first,
                second
            };
        }
    }

    public class Personnel
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public List<Personnel> Children { get; set; } = new List<Personnel>();
    }
}
