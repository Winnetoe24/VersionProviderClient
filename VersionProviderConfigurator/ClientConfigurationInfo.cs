using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionProviderConfigurator
{
    public class ClientConfigurationInfo : INotifyPropertyChanged
    {

        public ClientConfigurationInfo(string ConfigurationName, string ProgrammName, string ProgrammType)
        {
            this.ConfigurationName = ConfigurationName;
            this.ProgrammName = ProgrammName;
            this.ProgrammType = ProgrammType;
        }

        public string ConfigurationName { get; set; }

        public string RunnablePath { get; set; }

        public string ProgrammName { get; set; }
        public string ProgrammType { get; set;  }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
